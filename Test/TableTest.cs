using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    class TableTest
    {
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

            db.update("table", "column", "newdata", Operator.Equal, "data");

            string newdata = column.list[1];

            Assert.IsFalse(data == newdata);
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
