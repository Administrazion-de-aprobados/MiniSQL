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
    public class TableTest
    {
        [TestMethod]
        public void addToTable()
        {
            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");

            string name = "column";
            Library.Type type = Library.Type.Text;
            List<string> columnValues = new List<string>();
            columnValues.Add("prueba");


            Table table = db.Tables["table"];

            table.addToTable(name, type, columnValues);

            Assert.IsTrue(table.Columns.ContainsKey("column"));

        }
        [TestMethod]
        public void createColumn()
        {

            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");


            Table table = db.Tables["table"];
            table.createColumn("column1", Library.Type.Text);

            Assert.IsTrue(table.Columns.ContainsKey("column1"));
        }

        [TestMethod]
        public void selectToString() {

            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");
            Operator op = Operator.Equal;
            IList<string> listColumns = new List<string>();
            listColumns.Add("columnIntNumbers");
            

            string select = db.select(listColumns, "table", "columnIntNumbers", op, "1").selectToString();

            string result = "{'columnIntNumbers'} {'1'}";

            Assert.IsTrue(result.Equals(select));

        }
    }
}
