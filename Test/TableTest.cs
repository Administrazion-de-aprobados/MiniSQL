using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

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

            Table table = db.Tables["table"];

            Dictionary<string, Column> ht = table.Columns;

            Assert.IsTrue(ht.ContainsKey("column"));

        }

        [TestMethod]
        public void deleteColumn()
        {
            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");

            Table table = db.Tables["table"];
            table.deleteColumn("column");
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
          //string OldData = column.list[1];

            table.update("column", "newdata", Operator.Equal, "data");

            string newData = column.list[1];

            Assert.IsTrue(newData == "data");

        }

        [TestMethod]
        public void insert()
        {
            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");
            Table table = db.Tables["table"];
            table.insert("column", "newData");

            Column column = table.Columns["column"];

            Assert.IsTrue(column.list.Contains("newData"));
        }
    }
}
