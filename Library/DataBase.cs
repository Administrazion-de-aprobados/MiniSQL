using Library.Sentences.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum Operator { Greater, Less, Equal };
    public enum Type { Text, Int, Double };

    public enum Privilege {SELECT, DELETE, UPDATE, INSERT };

    public class DataBase
    {

        public Dictionary<string, User> Users;
        public Dictionary<string, Table> Tables;
        public Dictionary<string, SecurityProfile> SecProfiles;
        public User admin;
        public string Name;

        public DataBase(string name, string adminName, string pass)
        {
            Users = new Dictionary<string, User>();
            Tables = new Dictionary<string, Table>();
            SecProfiles = new Dictionary<string, SecurityProfile>();

            Name = name;
            admin = new User(adminName, pass);
            Users.Add(adminName, admin);
        }
        public DataBase()
        {
            Users = new Dictionary<string, User>();
            Tables = new Dictionary<string, Table>();
            SecProfiles = new Dictionary<string, SecurityProfile>();

            User user = new User("admin", "admin");
            Users.Add(user.Name, user);
        }


        public void createTable(string name, List<String> list)
        {
            if (!Tables.ContainsKey(name))
            {
                Table table = new Table(name);
                Tables.Add(name, table);

                foreach (string i in list)
                {
                    String[] splitedString = i.Split(' ');

                    Type type = dataType(splitedString[1].ToLower());

                    table.createColumn(splitedString[0], type);
                }
            }
            else
            {
                throw new Exception(Constants.TableAlreadyExists);
            }
        }

        public void dropTable(string name)
        {
            if (Tables.ContainsKey(name))
            {
                Tables.Remove(name);
            }
            else
            {
                throw new Exception(Constants.TableDoesNotExist);
            }
        }


        public Table select(IList<string> columnsNames, string tableName, string columnName, Operator op, string dataToCompare)
        {
            Table tableToReturn = new Table("select");
            if (Tables.ContainsKey(tableName))
            {
                Table table = Tables[tableName];

                IList<int> itemsPosition = where(tableName, columnName, op, dataToCompare);

                IList<string> columns = new List<string>();

                if (columnsNames.Contains("*"))
                {
                    foreach (string key in table.Columns.Keys)
                    {
                        columns.Add(key);
                    }
                }
                else
                {
                    columns = columnsNames;
                }

                foreach (string column in columns)
                {
                    if (table.Columns.ContainsKey(column))
                    {
                        Column coluumn = table.Columns[column];

                        List<string> list = new List<string>();

                        foreach (int num in itemsPosition)
                        {
                            list.Add(coluumn.list[num]);
                        }

                        tableToReturn.addToTable(coluumn.Name, coluumn.ColumnType, list);
                    }
                    else
                    {
                        throw new Exception(Constants.ColumnDoesNotExist);
                    }
                }
            }
            else
            {
                throw new Exception(Constants.TableDoesNotExist);
            }
            return tableToReturn;
        }

        public Table selectAll(IList<string> columnsNames, string tableName)
        {
            Table tableToReturn = new Table("select");
            if (Tables.ContainsKey(tableName))
            {
                Table table = Tables[tableName];

                IList<string> columns = new List<string>();

                if (columnsNames.Contains("*"))
                {
                    foreach (string key in table.Columns.Keys)
                    {
                        columns.Add(key);
                    }
                }
                else
                {
                    columns = columnsNames;
                }

                foreach (string column in columns)
                {

                    Column coluumn = table.Columns[column];

                    tableToReturn.Columns.Add(coluumn.Name, coluumn);
                }
            }
            else
            {
                throw new Exception(Constants.TableDoesNotExist);
            }

            return tableToReturn;
        }


        public void deleteData(string tableName, string columnName, Operator op, string ValueToCompare)
        {

            if (Tables.ContainsKey(tableName))
            {
                Table table = Tables[tableName];

                IList<int> list = where(tableName, columnName, op, ValueToCompare);

                foreach (int position in list)
                {
                    foreach (KeyValuePair<string, Column> key in table.Columns)
                    {
                        key.Value.deleteData(position);
                    }
                }
            }
            else
            {
                throw new Exception(Constants.TableDoesNotExist);
            }
        }

        public void update(string tableName, string columnName, string newData, string columnToCompare, Operator op, string valueToCompare)
        {

            if (Tables.ContainsKey(tableName))
            {

                Table tabTables = Tables[tableName];

                Dictionary<string, Column> tabColumn = tabTables.Columns;

                if (tabColumn.ContainsKey(columnName))
                {

                    Column col = tabColumn[columnName];

                    //IList<string> dataList = col.list;

                    //this list contais the positions of the data to change
                    IList<int> position = where(tableName, columnToCompare, op, valueToCompare);

                    foreach (int i in position)
                    {

                        //replace into the list from the column hashtable

                        col.list[i] = newData;

                    }


                }
                else
                {
                    throw new Exception(Constants.ColumnDoesNotExist);
                }

            }
            else
            {
                throw new Exception(Constants.TableDoesNotExist);
            }

        }

        public void insert(string nameTable, List<string> dataToInsert)
        {

            if (Tables.ContainsKey(nameTable))
            {

                Table tabTables = Tables[nameTable];

                Dictionary<string, Column> tabColumn = tabTables.Columns;

                Dictionary<string, Column>.KeyCollection col = tabColumn.Keys;

                int i = 0;

                foreach (var f in col)
                {

                    Boolean parar = true;
                    Column column = tabColumn[f];

                    while (parar == true && i < dataToInsert.Count)
                    {

                        string dataInsert = dataToInsert[i];

                        column.list.Add(dataInsert);

                        parar = false;
                    }

                    i++;

                }
            }
            else
            {

                throw new Exception(Constants.TableDoesNotExist);

            }
        }

        public void createSecurityProfile(string secProfName) {

            SecurityProfile securityProfile = new SecurityProfile(secProfName);

            if (!SecProfiles.ContainsKey(secProfName)) {

                SecProfiles.Add(secProfName, securityProfile);

            }
            else {

                throw new Exception(Constants.SecurityProfileAlreadyExists);
            }
        }

        public void dropSecurityProfile(string secProfName) {

            SecProfiles.Remove(secProfName);

            Dictionary<string, User>.KeyCollection users = Users.Keys;

            int i = 0;

            foreach (string user in users) {

                User us = Users[user];
                if (us.SecurityProfiles.Contains(secProfName)) {

                    us.SecurityProfiles.Remove(secProfName);

                }

            }


        }

        public void addUser(String name, String password, string securityProfileName)
        {

            User us = new User(name, password);


            if (!Users.ContainsKey(name))
            {
                Users.Add(name, us);

                SecurityProfile secProf = SecProfiles[securityProfileName];

                us.SecurityProfiles.Add(securityProfileName);

            }

            else
            {
                throw new Exception(Constants.SecurityUserAlreadyExists);
            }



        }

        public void deleteUser(string user) {

            if (Users.ContainsKey(user))
            {
                Users.Remove(user);
            }
            else {
                throw new Exception(Constants.SecurityUserDoesNotExist);
            }
        
        }

        public void grant(Privilege privilegeType, string table, string secProfile ) {

            if (SecProfiles.ContainsKey(secProfile))
            {
                SecurityProfile sp = SecProfiles[secProfile];

                if (Tables.ContainsKey(table))
                {
                    if (sp.Privileges.ContainsKey(table)) {

                        sp.Privileges[table].Add(privilegeType);
                    }
                    else {
                        List<Privilege> listaPrivilegios = new List<Privilege>();
                        listaPrivilegios.Add(privilegeType);
                        
                        sp.Privileges.Add(table,listaPrivilegios);

                    }

                }
                else
                {
                    throw new Exception(Constants.TableDoesNotExist);
                }
            }
            else {
                throw new Exception(Constants.SecurityProfileDoesNotExist);
            }
        }


        public void revoke(Privilege privilegeType, string table, string secProfile)
        {
            if (SecProfiles.ContainsKey(secProfile))
            {
                SecurityProfile sp = SecProfiles[secProfile];

                if (Tables.ContainsKey(table))
                {
                    if (sp.Privileges.ContainsKey(table)) {

                        sp.Privileges[table].Remove(privilegeType);
                    }
                    else {
                        throw new Exception("There is no privileges for this secProfile in that table");
                    }
                }
                else
                {
                    throw new Exception(Constants.TableDoesNotExist);
                }
            }
            else
            {
                throw new Exception(Constants.SecurityProfileDoesNotExist);
            }
        }


            //This code return a list of the positions the where should act
            public IList<int> where(string tableName, string columnName, Operator op, string valueToCompare)
        {
            IList<int> returnList = new List<int>();

            if (Tables.ContainsKey(tableName))
            {
                Table table = Tables[tableName];

                if (table.Columns.ContainsKey(columnName))
                {
                    Column column = table.Columns[columnName];

                    IList<string> list = column.list;
                    int i = 0;

                    foreach (string item in list)
                    {
                        if (compareValues(column.ColumnType, item, op, valueToCompare) == true)
                        {
                            returnList.Add(i);
                        }

                        i++;
                    }
                }
                else
                {
                    throw new Exception(Constants.ColumnDoesNotExist);
                }

            }
            else
            {
                throw new Exception(Constants.TableDoesNotExist);
            }



            return returnList;
        }

        // A method that returns true if the 
        public Boolean compareValues(Type type, string value, Operator op, string valueToCompare)
        {
            string operatoor = operatorTostring(op);
            Boolean isTrue = false;

            if (type == Type.Text)
            {
                if (operatoor.Equals("="))
                {
                    if (value.Equals(valueToCompare))
                    {
                        isTrue = true;
                    }
                }
                else
                {
                    //error because a text cant be > or <
                }

            }

            //for type int or double
            else if (type == Type.Double)
            {
                Double value1 = Double.Parse(value);
                Double valueToCompare1 = Double.Parse(valueToCompare);

                isTrue = compareTwoNumber<Double>(value1, operatoor, valueToCompare1);

            }
            else if (type == Type.Int)
            {
                int value1 = int.Parse(value);
                int valueToCompare1 = int.Parse(valueToCompare);

                isTrue = compareTwoNumber<int>(value1, operatoor, valueToCompare1);

            }
            return isTrue;
        }

        //This method compares two dobles and depending of the comparator will return true or false
        public Boolean compareTwoNumber<T>(T number1, string op, T number2) where T : System.IComparable<T>
        {
            Boolean isTrue = false;

            if (op.Equals("="))
            {
                if (number1.CompareTo(number2) == 0)
                {
                    isTrue = true;
                }

            }
            else if (op.Equals("<"))
            {
                if (number1.CompareTo(number2) < 0)
                {
                    isTrue = true;
                }
            }
            else if (op.Equals(">"))
            {
                if (number1.CompareTo(number2) > 0)
                {
                    isTrue = true;
                }
            }

            return isTrue;
        }



        public void write()
        {

            if (File.Exists(Name))
            {
                deletefile(Name);
            }

            //create the txt
            StreamWriter sw = File.CreateText(Name);

            //admin name and password for the txt
            sw.WriteLine(admin.Name + "," + admin.Password);

            //iterates the keys from the hastable
            foreach (KeyValuePair<string, Table> entry in Tables)
            {
                string tableName = entry.Key;
                sw.WriteLine(tableName);

                Table table = entry.Value;
                foreach (KeyValuePair<string, Column> entry2 in table.Columns)
                {
                    string columnName = entry2.Key; //the key from the line 
                    Column column = entry2.Value;//the value from the line 
                    string line = columnName + "," + column.ColumnType;

                    foreach (string value in column.list)
                    {
                        line = line + "," + value;
                    }
                    sw.WriteLine(line);


                }
                sw.WriteLine(" ");

            }
            sw.Close();

        }

        public void writeSecurity()
        {
            string name = Name + "Security";

            if (File.Exists(name))
                deletefile(name);

            StreamWriter sw = File.CreateText(name);

            foreach(SecurityProfile sp in SecProfiles.Values)
            {
                sw.WriteLine(sp.Name);
                foreach (string table in sp.Privileges.Keys)
                {
                    string line = table+";";

                    List<Privilege> list = sp.Privileges[table];

                    foreach (Privilege type in list)
                    {

                        string stringType = privilegeToString(type);
                        line = line + stringType + ",";

                    }
                    sw.WriteLine(line);
                }
                sw.WriteLine(" ");

            }
            sw.Close();

        }


        public void load(string txtName)
        {

            if (File.Exists(txtName))
            {

                string[] filas = File.ReadAllLines(txtName);

                string[] line1 = filas[0].Split(',');

                string adminName = line1[0];

                string pass = line1[1];

                User admiin = new User(adminName, pass);

                admin = admiin;


                for (int i = 1; i < filas.Length; i++)
                {
                    string tableName = filas[i];
                    Table tab = new Table(tableName);
                    i++;
                    while (filas[i] != " ")
                    {
                        String[] line = filas[i].Split(',');
                        string columnName = line[0];
                        string datat = line[1];
                        datat = datat.ToLower();
                        List<string> data = new List<string>();

                        Type tp = dataType(datat);
                        for (int j = 2; j < line.Length; j++)
                        {
                            data.Add(line[j]);
                        }

                        tab.addToTable(columnName, tp, data);
                        i++;
                    }
                    Tables.Add(tableName, tab);
                }
            }
        }

        public void loadSecurity(string txtname)
        {
            if (File.Exists(txtname))
            {

                string[] lines = File.ReadAllLines(txtname);

                for (int i = 0; i < lines.Length; i++)
                {
                    string profile = lines[i];

                    createSecurityProfile(profile);
                    i++;

                    while(lines[i]!=" ")
                    {
                        string[] line = lines[i].Split(';');

                        string table = line[0];

                        string[] types = line[1].Split(',');

                        foreach(string type in types)
                        {
                            Privilege typee = Query.stringToType(type);

                            grant(typee, table, profile);

                        }

                        i++;

                    }

                }
            }
        }


        public string privilegeToString(Privilege type)
        {

            if (type == Privilege.SELECT)
            {
                return "SELECT";
            }
            else if (type == Privilege.DELETE)
            {
                return "DELETE";
            }
            else if(type == Privilege.INSERT)
            {
                return "INSERT";
            }
            else if(type == Privilege.UPDATE)
            {
                return "UPDATE";
            }

            return Constants.Error;
        }


        public string operatorTostring(Operator op)
        {
            string operatoor = null;

            if (op == Operator.Equal)
            {
                operatoor = "=";
            }
            else if (op == Operator.Greater)
            {
                operatoor = ">";
            }
            else if (op == Operator.Less)
            {
                operatoor = "<";
            }

            return operatoor;
        }



        public Type dataType(string loadData)
        {

            Type tipo = Type.Text;
            if (loadData == "text")
            {

                tipo = Type.Text;
            }
            else if (loadData == "int")
            {
                tipo = Type.Int;
            }

            else
            {

                tipo = Type.Double;
            }

            return tipo;

        }

        public void deletefile(string name)
        {
            File.Delete(name);
        }



        public string output(string input, User user) {

            Sentence sentence = Query.parse(input);

            string output = "";
            try
            {
                if (sentence is Statements)
                {
                    Statements statement = sentence as Statements;

                    if (statement is Select)
                    {

                        Select sel = statement as Select;

                        IList<string> columnsNames = sel.listColumns;
                        string tableName = sel.tableName;
                        Where where = sel.sentenceWhere;
                        Operator op = where.op;
                        string columnName = where.col;
                        string dataToCompare = where.colData;

                        if (hasPrivilege(user, tableName, Privilege.SELECT))
                        {
                            output = select(columnsNames, tableName, columnName, op, dataToCompare).selectToString();
                        }
                        else
                        {
                            output = Constants.SecurityNotSufficientPrivileges;
                        }
                    }

                    else if (statement is SelectAll)
                    {
                        SelectAll sel = statement as SelectAll;

                        IList<string> columnsNames = sel.listColumns;
                        string tableName = sel.tableName;

                        if (hasPrivilege(user, tableName, Privilege.SELECT))
                        {
                            output = selectAll(columnsNames, tableName).selectToString();
                        }
                        else
                        {
                            output = Constants.SecurityNotSufficientPrivileges;
                        }
                    }

                    else if (statement is Delete)
                    {

                        Delete delete = statement as Delete;

                        string tabName = delete.tableName;
                        Where where = delete.sentenceWhere;
                        string column = where.col;
                        Operator op = where.op;
                        string data = where.colData;
                      
                        if (hasPrivilege(user, tabName, Privilege.DELETE))
                        {
                            deleteData(tabName, column, op, data);
                            output = Constants.TupleDeleteSuccess;
                        }
                        else
                        {
                            output = Constants.SecurityNotSufficientPrivileges;
                        }
                    }

                    else if (statement is Insert)
                    {

                        Insert ins = statement as Insert;
                        string nameTable = ins.tableName;
                        List<string> dataToInsert = ins.row;

                        if (hasPrivilege(user, nameTable, Privilege.INSERT))
                        {
                            insert(nameTable, dataToInsert);
                            output = Constants.InsertSuccess;
                        }
                        else
                        {
                            output = Constants.SecurityNotSufficientPrivileges;
                        }

                    }

                    else if (statement is Update)
                    {

                        Update upd = statement as Update;
                        string tableName = upd.tableName;
                        List<string> columnNames = upd.column;
                        List<string> newValues = upd.newValue;
                        Where where = upd.sentenceWhere;
                        string columnToCompare = where.col;
                        Operator op = where.op;
                        string data = where.colData;

                        if (hasPrivilege(user, tableName, Privilege.UPDATE))
                        {
                            for (int i = 0; i < columnNames.Count; i++)
                            {

                                string columnName = columnNames[i];
                                string newData = newValues[i];
                                update(tableName, columnName, newData, columnToCompare, op, data);
                            }

                            output = Constants.TupleUpdateSuccess;
                        }
                        else
                        {
                            output = Constants.SecurityNotSufficientPrivileges;
                        }
                    }

                    else if (statement is DropTable)
                    {

                        DropTable drop = statement as DropTable;
                        string tableName = drop.tableName;

                        if (user.Name.Equals("admin"))
                        {
                            dropTable(tableName);
                            output = Constants.TableDroppedSucess;
                        }
                        else
                        {
                            output = Constants.SecurityNotSufficientPrivileges;
                        }
                    }

                    else if (statement is CreateTable)
                    {

                        CreateTable create = statement as CreateTable;
                        string tableName = create.tableName;
                        List<string> colNames = create.ListOfColumns;

                        if (user.Name.Equals("admin"))
                        {
                            createTable(tableName, colNames);
                            output = Constants.CreateTableSuccess;
                        }
                        else
                        {
                            output = Constants.SecurityNotSufficientPrivileges;
                        }

                    }
                }
                else if(sentence is SecurityQueries)
                {
                    SecurityQueries securityQueries = sentence as SecurityQueries;


                    if(!user.Equals(admin.Name) && !user.Password.Equals(admin.Password))
                    {
                        output = Constants.SecurityNotSufficientPrivileges;
                    }
                    else
                    {
                        if(securityQueries is AddUser)
                        {
                            AddUser addUseer = securityQueries as AddUser;

                            string name = addUseer.User;
                            string pass = addUseer.Password;
                            string security = addUseer.SecurityProfileName;

                            addUser(name, pass, security);
                            output = Constants.SecurityUserAdded;
                        }

                        else if (securityQueries is DeleteUser)
                        {
                            DeleteUser deleteeUser = securityQueries as DeleteUser;

                            string name = deleteeUser.User;

                            deleteUser(name);
                            output = Constants.SecurityUserDeleted;
                        }

                        else if (securityQueries is CreateSecurityProfile)
                        {
                            CreateSecurityProfile create = securityQueries as CreateSecurityProfile;

                            string security = create.SecurityProfileName;

                            createSecurityProfile(security);
                            output = Constants.SecurityProfileCreated;

                        }

                        else if (securityQueries is DropSecurityProfile)
                        {
                            DropSecurityProfile drop = securityQueries as DropSecurityProfile;

                            string name = drop.SecurityProfileName;

                            dropSecurityProfile(name);
                            output = Constants.SecurityProfileDeleted;

                        }

                        else if (securityQueries is GrantPrivilege)
                        {
                            GrantPrivilege graant = securityQueries as GrantPrivilege;

                            Privilege type = graant.Type;
                            string table = graant.Table;
                            string security = graant.SecurityProfileName;

                            grant(type, table, security);
                            output = Constants.SecurityPrivilegeGranted;
                        }

                        else if (securityQueries is RevokePrivilege)
                        {
                            RevokePrivilege revooke = securityQueries as RevokePrivilege;

                            Privilege type = revooke.Type;
                            string table = revooke.Table;
                            string security = revooke.SecurityProfileName;

                            revoke(type, table, security);
                            output = Constants.SecurityPrivilegeRevoked;
                        }

                    }

                }
                else
                {
                    output = Constants.WrongSyntax;
                }

            }
            catch (Exception e)
            {

                output = e.Message;

            }

            return output;
        }


        public Boolean existUser(string name, string pass)
        { 
            if (Users.ContainsKey(name))
            {
                User user = Users[name];
                if (user.Password.Equals(pass))
                    return true;
            }
            return false;
        }

        public Boolean hasPrivilege(User user, string table, Privilege type)
        {
            if (user.Name.Equals(admin.Name) && user.Name.Equals(admin.Password))
                return true;


            foreach(string profile in user.SecurityProfiles)
            {
                if (SecProfiles.ContainsKey(profile))
                {
                    SecurityProfile securityProfile = SecProfiles[profile];

                    if (securityProfile.Privileges.ContainsKey(table))
                        if (securityProfile.Privileges[table].Contains(type))
                            return true;
                }
            }
            return false;
        }

    }
}



