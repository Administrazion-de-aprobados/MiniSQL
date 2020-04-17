using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;
using System.IO;
using Library;
using Library.Sentences.Security;

namespace Test
{
    [TestClass]
    public class QueryTest
    {
        [TestMethod]
        public void parse()
        {

            Assert.IsTrue(parseSelect());
            Assert.IsTrue(parseDelete());
            Assert.IsTrue(parseInsert());
            Assert.IsTrue(parseUpdate());
            Assert.IsTrue(parseCreatetable());
            Assert.IsTrue(parseDropTable());

            Assert.IsTrue(parseAddUser());
            Assert.IsTrue(parseDelete());
            Assert.IsTrue(parseCreateSecurityProfile());
            Assert.IsTrue(parseDropSecurityProfile());
            Assert.IsTrue(parseGrantPrivilege());
            Assert.IsTrue(parseRevokePrivilege());

        }

        public Boolean parseSelect()
        {

            Sentence sentence = Query.parse("SELECT column FROM table WHERE 1>1;");

            Select select = sentence as Select;

            Where where = new Where("1", Operator.Greater, "1");

            List<string> list = new List<string>();
            list.Add("column");


            if (select.tableName.Equals("table"))
            {
                if (select.sentenceWhere.col == where.col && select.sentenceWhere.op == where.op && select.sentenceWhere.colData == where.colData)
                {
                    if (select.listColumns[0] == list[0])
                        return true;
                    else
                        return false;
                }
                else
                    return false;

            }
            return false;
        }

        public Boolean parseDelete()
        {
            Sentence sentence = Query.parse("DELETE FROM table WHERE 1=1;");

            Delete delete = sentence as Delete;

            Where where = new Where("1", Operator.Equal, "1");

            if (delete.tableName.Equals("table"))
            {
                if (delete.sentenceWhere.col == where.col && delete.sentenceWhere.op == where.op && delete.sentenceWhere.colData == where.colData)
                    return true;
                else
                    return false;
            }


            return false;
        }

        public Boolean parseInsert()
        {
            Sentence sentence = Query.parse("INSERT INTO table VALUES ('prueba1','prueba2');");

            //Statements statement = sentence as Statements;

            Insert insert = sentence as Insert;

            List<String> list = new List<string>();

            list.Add("prueba1");
            list.Add("prueba2");

            if (insert.tableName.Equals("table"))
            {
                if (insert.row[0] == list[0] && insert.row[1] == list[1])
                    return true;
                else
                    return false;
            }

                return false;
        }

        public Boolean parseUpdate()
        {
            Sentence sentece = Query.parse("UPDATE table SET columna1=1,columna2=2,columna3=3 WHERE 1<1;");

            Update update = sentece as Update;

            List<string> columns = new List<string>();
            columns.Add("columna1");
            columns.Add("columna2"); 
            columns.Add("columna3");

            List<String> data = new List<string>();
            data.Add("1");
            data.Add("2");
            data.Add("3");

            Where where = new Where("1", Operator.Less, "1");

            if (update.tableName.Equals("table"))
                if (update.column[0] == columns[0] && update.column[1] == columns[1] && update.column[2] == columns[2])
                    if (update.newValue[0] == data[0] && update.newValue[1] == data[1] && update.newValue[2] == data[2])
                        if (update.sentenceWhere.col == where.col && update.sentenceWhere.op == where.op && update.sentenceWhere.colData == where.colData)
                            return true;

                return false;
        }

        public Boolean parseCreatetable()
        {
            Sentence sentence = Query.parse("CREATE TABLE table (column1 INT,column2 TEXT,column3 DOUBLE);");

            CreateTable createTable = sentence as CreateTable;

            if (createTable.tableName.Equals("table"))
                if (createTable.ListOfColumns[0] == "column1 INT" && createTable.ListOfColumns[1] == "column2 TEXT" && createTable.ListOfColumns[2] == "column3 DOUBLE")
                    return true;

            return false;
        }


        public Boolean parseDropTable()
        {
            Sentence sentence = Query.parse("DROP TABLE table;");

            DropTable dropTable = sentence as DropTable;

            if (dropTable.tableName.Equals("table"))
                return true;


            return false;
        }

        public Boolean parseCreateSecurityProfile()
        {
            Sentence sentence = Query.parse("CREATE SECURITY PROFILE Employee;");

            CreateSecurityProfile CreateSecurityProfile = sentence as CreateSecurityProfile;

            if (CreateSecurityProfile.SecurityProfileName.Equals("Employee"))
                return true;

            return false;
        }

        public Boolean parseDropSecurityProfile()
        {
            Sentence sentence = Query.parse("DROP SECURITY PROFILE Employee;");

            DropSecurityProfile DropSecurityProfile = sentence as DropSecurityProfile;

            if (DropSecurityProfile.SecurityProfileName.Equals("Employee"))
                return true;

            return false;
        }

        public Boolean parseGrantPrivilege()
        {
            Sentence sentence = Query.parse("GRANT SELECT ON Employees_Public TO Employee;");

            GrantPrivilege grantPrivilege = sentence as GrantPrivilege;

            if (grantPrivilege.SecurityProfileName.Equals("Employee"))
                if (grantPrivilege.Table.Equals("Employees_Public"))
                    if (grantPrivilege.Type == Privilege.SELECT)
                        return true;

            return false;
        }

        public Boolean parseRevokePrivilege()
        {
            Sentence sentence = Query.parse("REVOKE SELECT ON Employees_Public TO Employee;");

            RevokePrivilege revokePrivilege = sentence as RevokePrivilege;

            if (revokePrivilege.SecurityProfileName.Equals("Employee"))
                if (revokePrivilege.Table.Equals("Employees_Public"))
                    if (revokePrivilege.Type == Privilege.SELECT)
                        return true;

            return false;
        }

        public Boolean parseAddUser()
        {
            Sentence sentence = Query.parse("ADD USER ('Eva','1234',Employee);");

            AddUser addUser = sentence as AddUser;

            if (addUser.User.Equals("Eva"))
                if (addUser.Password.Equals("1234"))
                    if (addUser.SecurityProfileName.Equals("Employee"))
                        return true;

            return false;
        }

        public Boolean parseDeleteUser()
        {
            Sentence sentence = Query.parse("DELETE USER Eva;");

            DeleteUser deleteUser = sentence as DeleteUser;

            if (deleteUser.User.Equals("Eva"))
                return true;

            return false;
        }

    }
}
