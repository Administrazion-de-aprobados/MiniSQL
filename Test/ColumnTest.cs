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
    public class ColumnTest
    {
        //[TestMethod]
        public void searchData()
        {
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");


        }

       // [TestMethod]
        public void deleteData()
        {
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            Table table = db.Tables["table"];
            Column column = table.Columns["column"];
            column.deleteData(0);
            Assert.IsTrue(column.list[0]!="data");
        }

       // [TestMethod]
        /*public void updateData()
        {
            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");

            Table table = db.Tables["table"];
            Column column = table.Columns["column"];
            string data = column.list[0];

            column.updateData("newdata", Operator.Equal, "data");


            string newdata = column.list[0];

            Assert.IsFalse(data == newdata);
        }
        */
       // [TestMethod]
        public void insert()
        {
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");

            Table table = db.Tables["table"];
            Column column = table.Columns["column"];

            column.insert("newData");

            Assert.IsTrue(column.list.Contains("newData"));
        }
    }
}
