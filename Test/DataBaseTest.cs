using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;

namespace Test
{
    [TestClass]
    public class DataBaseTest
    {
        [TestMethod]
        public void createTable()
        {
            Library.DataBase bd = new Library.DataBase("BD", "admin", "admin");

            bd.BDcreatioon(bd);
           
        }

        [TestMethod]
        public void dropTable()
        {
            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");

            db.dropTable("table");

           Assert.IsFalse(db.Tables.ContainsKey("table"));


        }

        [TestMethod]
        public void searchColumnName()
        {
            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");

        }

        [TestMethod]
        public void deleteColumn()
        {

            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");


            db.deleteColumn("table", "column");
            Table table = db.Tables["table"];
            Assert.IsFalse(table.Columns.ContainsKey("column"));

        }

        [TestMethod]
        public void update()
        {
            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");

            Table table = db.Tables["table"];
            Column column = table.Columns["column"];
            string data = column.list[1];

            db.update("table", "column", "newdata",Equal, "data");


        }

        [TestMethod]
        public void insert()
        {

        }
    }
}
