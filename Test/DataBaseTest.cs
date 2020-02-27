using System;
using System.Collections.Generic;
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
            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");

            db.createTable("newTable");

            Assert.IsTrue(db.Tables.ContainsKey("newTable"));
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

            Assert.IsTrue(db.searchColumnName("column"));
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
        public void deleteData()
        {

            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");

            db.deleteData("table", "column", Operator.Equal, "data");

            Table table = db.Tables["table"];
            Column column = table.Columns["column"];
            Assert.IsFalse(column.list.Contains("data"));

        }

        [TestMethod]
        public void select()
        {
            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");


            IList<string> list = new List<string>();
            list.Add("column");

            Dictionary<string, Column> table = db.select(list, "table", "column", Operator.Equal, "data");

            Assert.IsTrue(table.ContainsKey("column"));

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

            db.update("table", "column", "newdata",Operator.Equal, "data");

            string newdata = column.list[1];

            Assert.IsFalse("newdata" == newdata);
        }

        [TestMethod]
        public void insert()
        {

            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");

            db.insert("table", "column", "newData");


            Table table = db.Tables["table"];
            Column column = table.Columns["column"];

            Assert.IsFalse(column.list.Contains("newData"));
        }
    }
}
