using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;
using System.IO;

namespace Test
{
    [TestClass]
    public class DataBaseTest
    {
        [TestMethod]
        public void createTable()
        {
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            List<String> list = new List<string>();

            db.createTable("newTable", list);

            Assert.IsTrue(db.Tables.ContainsKey("newTable"));
        }

        [TestMethod]
        public void dropTable()
        {
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            db.dropTable("table");

            Assert.IsFalse(db.Tables.ContainsKey("table"));


        }




        [TestMethod]
        public void deleteData()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            db.deleteData("table", "column", Operator.Equal, "data");

            Table table = db.Tables["table"];
            Column column = table.Columns["column"];
            Assert.IsFalse(column.list.Contains("data"));

        }

        [TestMethod]
        public void select()
        {
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");


            IList<string> list = new List<string>();
            list.Add("column");
            list.Add("columnIntNumbers");

            Table table = db.select(list, "table", "column", Operator.Equal, "data");


            Assert.IsTrue(table.Columns["columnIntNumbers"].list.Contains("1"));
            //Assert.IsTrue(table.Columns.ContainsKey("column"));

        }


        [TestMethod]
        public void update()
        {
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            Table table = db.Tables["table"];
            Column column = table.Columns["column"];

            db.update("table", "column", "newdata", "column", Operator.Equal, "data");

            string newdata = column.list[0];

            Assert.IsTrue("newdata" == newdata);
        }

        [TestMethod]
        public void insert()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            List<string> lista = new List<string>();
            lista.Add("datos");
            lista.Add("3");
            lista.Add("2.3");

            db.insert("table", lista);


            Table table = db.Tables["table"];
            Column column = table.Columns["columnIntNumbers"];

            Assert.IsTrue(column.list.Contains("3"));
        }

        [TestMethod]
        public void where()
        {
            Assert.IsTrue(whereEqual());
            Assert.IsTrue(whereGreater());
            Assert.IsTrue(whereLess());
        }

        public Boolean whereEqual()
        {
            Boolean isTrue = false;
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            IList<int> lista = new List<int>();
            lista.Add(0);

            IList<int> list = db.where("table", "column", Operator.Equal, "data");

            if (lista[0] == list[0])
            {
                isTrue = true;
            }
            return isTrue;
        }

        public Boolean whereGreater()
        {
            Boolean isTrue = false;
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            IList<int> lista = new List<int>();
            lista.Add(2);
            lista.Add(3);

            IList<int> list = db.where("table", "columnIntNumbers", Operator.Greater, "2");

            if (lista[0] == list[0] && lista[1] == list[1])
            {
                isTrue = true;
            }

            return isTrue;
        }

        public Boolean whereLess()
        {
            Boolean isTrue = false;
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            IList<int> lista = new List<int>();
            lista.Add(0);
            lista.Add(1);

            IList<int> list = db.where("table", "columnDoubleNumbers", Operator.Less, "3.00");

            if (lista[0] == list[0] && lista[1] == list[1])
            {
                isTrue = true;
            }

            return isTrue;
        }


        [TestMethod]
        public void writeandLoad()
        {

            DataBase database = new DataBase("BD", "admin", "admin");

            List<String> list = new List<string>();
            list.Add("column TEXT");

            database.createTable("table", list);

            Table table = database.Tables["table"];
            //table.createColumn("column", Library.Type.Text);

            Column column = table.Columns["column"];
            column.list.Add("data");

            database.write();
            
            DataBase db = DataBase.load("BD");

            if (db.Tables.ContainsKey("table"))
            {
                Table table1 = db.Tables["table"];

                if (table1.Columns.ContainsKey("column"))
                {
                    Column column1 = table1.Columns["column"];
                    if (column1.list.Contains("data"))
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }
                else
                {
                    Assert.IsTrue(false);
                }
            }
            else
            {
                Assert.IsTrue(false);
            }

        }

        public void writeloadSecurity()
        {




        }



        [TestMethod]
        public void delete()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            db.deletefile("BD");

            Assert.IsFalse(File.Exists("BD"));

        }

        [TestMethod]
        public void operatorTostringEqual()
        {
            DataBase database = new DataBase("BD", "admin", "admin");
            Operator op = Operator.Equal;

            Assert.IsTrue(database.operatorTostring(op).Equals("="));


        }
        [TestMethod]
        public void operatorTostringGreater()
        {
            DataBase database = new DataBase("BD", "admin", "admin");
            Operator op = Operator.Greater;

            Assert.IsTrue(database.operatorTostring(op).Equals(">"));


        }
        [TestMethod]
        public void operatorTostringLess()
        {
            DataBase database = new DataBase("BD", "admin", "admin");
            Operator op = Operator.Less;

            Assert.IsTrue(database.operatorTostring(op).Equals("<"));


        }

        [TestMethod]
        public void dataTypeText()
        {
            DataBase database = new DataBase("BD", "admin", "admin");
            string tipo = "text";

            Assert.IsTrue(database.dataType(tipo).Equals(Library.Type.Text));


        }
        [TestMethod]
        public void dataTypeInt()
        {
            DataBase database = new DataBase("BD", "admin", "admin");
            string tipo = "int";


            Assert.IsTrue(database.dataType(tipo).Equals(Library.Type.Int));


        }
        [TestMethod]
        public void dataTypeDouble()
        {
            DataBase database = new DataBase("BD", "admin", "admin");
            string tipo = "double";


            Assert.IsTrue(database.dataType(tipo).Equals(Library.Type.Double));


        }

        [TestMethod]
        public void output()
        {
            outputSelect();
            outputDelete();
            outputInsert();
            outputUpdate();
            outputCreateTable();
            outputDropTable();

            outputAddUser();
            outputDeleteUser();
            outputCreateSecurityProfile();
            outputDropSecurityProfile();
            outputGrantPrivilege();
            outputRevokePrivilege();
        }

        public void outputSelect()
        {
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            string input = "SELECT columnIntNumbers FROM table WHERE columnIntNumbers=1;";

            string output = db.output(input,db.admin);

            Assert.IsTrue(output.Equals("['columnIntNumbers'] {'1'}"));
        }
        public void outputDelete()
        {
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            string input = "DELETE FROM table WHERE columnIntNumbers=1;";

            string output = db.output(input, db.admin);

            Assert.IsTrue(output.Equals(Constants.TupleDeleteSuccess));

        }

        public void outputInsert()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            string input = "INSERT INTO table VALUES ('data',5,5.05);";

            string output = db.output(input, db.admin);

            Assert.IsTrue(output.Equals(Constants.InsertSuccess));


        }

        public void outputUpdate()
        {


            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            string input = "UPDATE table SET columnIntNumbers=5 WHERE columnIntNumbers=1;";

            string output = db.output(input, db.admin);

            Assert.IsTrue(output.Equals(Constants.TupleUpdateSuccess));

        }

        public void outputCreateTable()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            string input = "CREATE TABLE table444 (column1 DOUBLE,column2 TEXT);";

            string output = db.output(input, db.admin);

            Assert.IsTrue(output.Equals(Constants.CreateTableSuccess));

        }
        public void outputDropTable()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            string input = "DROP TABLE table;";

            string output = db.output(input, db.admin);

            Assert.IsTrue(output.Equals(Constants.TableDroppedSucess));

        }

        public void outputCreateSecurityProfile()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            string input = "CREATE SECURITY PROFILE Employee;";

            string output = db.output(input, db.admin);

            Assert.IsTrue(output.Equals(Constants.SecurityProfileCreated));

        }

        public void outputDropSecurityProfile()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");
            db.loadSecurity("BDSecurity");
            db.loadUsers("BDUsers");

            string input = "DROP SECURITY PROFILE profile1;";

            string output = db.output(input, db.admin);

            Assert.IsTrue(output.Equals(Constants.SecurityProfileDeleted));

        }

        public void outputGrantPrivilege()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");
            db.loadSecurity("BDSecurity");
            db.loadUsers("BDUsers");

            string input = "GRANT SELECT ON table2 TO profile2;";

            string output = db.output(input, db.admin);

            Assert.IsTrue(output.Equals(Constants.SecurityPrivilegeGranted));

        }

        public void outputRevokePrivilege()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");
            db.loadSecurity("BDSecurity");
            db.loadUsers("BDUsers");

            string input = "REVOKE SELECT ON table TO profile1;";

            string output = db.output(input, db.admin);

            Assert.IsTrue(output.Equals(Constants.SecurityPrivilegeRevoked));

        }

        public void outputAddUser()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");
            db.loadSecurity("BDSecurity");
            db.loadUsers("BDUsers");

            string input = "ADD USER ('Eva','1234',profile2);";

            string output = db.output(input, db.admin);

            Assert.IsTrue(output.Equals(Constants.SecurityUserAdded));

        }

        public void outputDeleteUser()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");
            db.loadSecurity("BDSecurity");
            db.loadUsers("BDUsers");

            string input = "DELETE USER yeray;";

            string output = db.output(input, db.admin);

            Assert.IsTrue(output.Equals(Constants.SecurityUserDeleted));

        }







        [TestMethod]
        public void createSecurityProfile()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            string secProf = "SECURITY";


            db.createSecurityProfile(secProf);

            SecurityProfile sp = db.SecProfiles[secProf];

            Assert.IsTrue(secProf.Equals(sp.Name));

        }
        [TestMethod]
        public void addUser()
        {
            string name = "nombre";
            string password = "123456";
            string secProf = "SECURITY";


            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            db.createSecurityProfile("SECURITY");

            db.addUser(name, password, secProf);

            User us = db.Users[name];

            Assert.IsTrue(name.Equals(us.Name) && us.SecurityProfiles.Contains(secProf));
        }

        [TestMethod]
        public void dropSecurityProfile()
        {

            string secProfileName = "SECURITY";

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            db.createSecurityProfile("SECURITY");
            db.addUser("Juan", "123456", "SECURITY");

            db.dropSecurityProfile(secProfileName);

            Assert.IsFalse(db.SecProfiles.ContainsKey(secProfileName));

            Dictionary<string, User>.KeyCollection users = db.Users.Keys;

            int i = 0;

            foreach (string user in users)
            {

                User us = db.Users[user];
                if (!us.SecurityProfiles.Contains(secProfileName))
                {

                    Assert.IsFalse(us.SecurityProfiles.Contains(secProfileName));

                }

            }
        }
        [TestMethod]
        public void deleteUser()
        {
            string user = "Juan";
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            db.createSecurityProfile("SECURITY");
            db.addUser("Juan", "123456", "SECURITY");

            db.deleteUser(user);

            Assert.IsFalse(db.Users.ContainsKey(user));
        }
        [TestMethod]
        public void grant()
        {
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            db.createSecurityProfile("SECURITY");
            Library.Privilege privilegeType = Library.Privilege.SELECT;
            string table = "table";
            string securityProfile = "SECURITY";

            db.grant(privilegeType,table,securityProfile);

           

            Assert.IsTrue(db.SecProfiles[securityProfile].Privileges[table].Contains(privilegeType));

        }

        [TestMethod]
        public void revoke()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            db.createSecurityProfile("SECURITY");
            Library.Privilege privilegeType = Library.Privilege.SELECT;
            string table = "table";
            string securityProfile = "SECURITY";

            db.grant(privilegeType, table, securityProfile);

            db.revoke(privilegeType, table, securityProfile);

            Assert.IsFalse(db.SecProfiles[securityProfile].Privileges[table].Contains(privilegeType));

        }

        [TestMethod]
        public void existUser()
        {

            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");
            db.loadSecurity("BDSecurity");
            db.loadUsers("BDUsers");

            Assert.IsTrue(db.existUser("yeray", "321"));
            Assert.IsFalse(db.existUser("Borja", "123"));
            Assert.IsFalse(db.existUser("ane", "789"));

        }

        [TestMethod]
        public void hasPrivilege()
        {
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");
            db.loadSecurity("BDSecurity");
            db.loadUsers("BDUsers");

            User user = db.Users["yeray"];


            Assert.IsTrue(db.hasPrivilege(user, "table2", Privilege.DELETE));
            Assert.IsFalse(db.hasPrivilege(user, "table", Privilege.SELECT));
            Assert.IsFalse(db.hasPrivilege(user, "table2", Privilege.SELECT));

        }



    }
}
