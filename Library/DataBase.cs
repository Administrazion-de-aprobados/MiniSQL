using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum Operator { Greater, Less, Equal};
    public enum Type {Text, Int, Double};


    public class DataBase
    {
        public Dictionary<string, User> Users;
        public Dictionary<string, Table> Tables;
        private Admin admin;
        public string Name;

        public DataBase(string name, string adminName, string pass)
        {
            Users = new Dictionary<string, User>();
            Tables = new Dictionary<string, Table>();

            Name = name;
            admin = new Admin(adminName, pass);

        }
        public DataBase()
        {
            Users = new Dictionary<string, User>();
            Tables = new Dictionary<string, Table>();
        }


        public void createTable(string name)
        {
            Table table = new Table(name);
            Tables.Add(name, table);
        }

        public void dropTable(string name)
        {
            Tables.Remove(name);
        }


        // select all? no se si hay que hacerlo o no
        public Table select(IList<string> columnsNames, string tableName, string columnName, Operator op, string dataToCompare)
        {
            Table tableToReturn = new Table("select");
            if (Tables.ContainsKey(tableName))
            {
                Table table = Tables[tableName];

                IList<int> itemsPosition = where(tableName, columnName, op, dataToCompare);

                foreach (string column in columnsNames)
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
                        //error columns doesnt exist
                    }
                }
            }
            else
            {
                // error table doesnt exist
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

        }

        public void update(string tableName, string columnName, string dataToUpdate, Operator op, string valueToCompare)
        {

        }

        public void insert(string nameTable, string nameCol, string dataToIInsert)
        {



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
                    //this should return an error from constatns but as we dont know how we will do the console we will implement this when we start with the console code
                }

            }
            else
            {
                //this should return an error from constatns but as we dont know how we will do the console we will implement this when we start with the console code
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
            else if(type == Type.Int)
            {
                int value1 = int.Parse(value);
                int valueToCompare1 = int.Parse(valueToCompare);

                isTrue = compareTwoNumber<int>(value1, operatoor, valueToCompare1);

            }
            return isTrue;
        }

        //This method compares two dobles and depending of the comparator will return true or false
        public Boolean compareTwoNumber<T>(T number1, string op,T number2) where T : System.IComparable<T>
        {
            Boolean isTrue = false;

            if (op.Equals("="))
            {
                if (number1.CompareTo(number2)==0)
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
             
            if(File.Exists(Name))
            {
                
                deletefile(Name);
            }

            //create the txt
            StreamWriter sw = File.CreateText(Name);

            //admin name and password for the txt
            sw.WriteLine(admin.Name + "," + admin.Password);
            
            //iterates the keys from the hastable
            foreach(KeyValuePair<string, Table> entry in Tables)
            {
                string tableName = entry.Key;
                sw.WriteLine(tableName);

                Table table = entry.Value;
                foreach(KeyValuePair<string, Column> entry2 in table.Columns)
                {
                    string columnName = entry2.Key; //the key from the line 
                    Column column = entry2.Value;//the value from the line 
                    string line = columnName + "," + column.ColumnType;

                    foreach(string value in column.list)
                    {
                         line = line +","+ value;
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

                Admin admiin = new Admin(adminName, pass);

                admin = admiin;
                Boolean newTable = true;

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



        public Type dataType(string loadData) {

            Type tipo = Type.Text;
            if (loadData == "text")
            {

                tipo = Type.Text;
            }
            else if (loadData == "int")
            {
                tipo = Type.Int;
            }
            else {

                tipo = Type.Double;
            }

            return tipo;

        }

        public void deletefile(string name)
        {
            File.Delete(name);
        }
    }
}
