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

       // [TestMethod]
        public void searchColumnName()
        {
            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");

            Assert.IsTrue(db.searchColumnName("column"));
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
            list.Add("columnIntNumbers");

           Table table = db.select(list, "table", "column", Operator.Equal, "data");
           

            Assert.IsTrue(table.Columns["columnIntNumbers"].list.Contains("1"));
            Assert.IsTrue(table.Columns.ContainsKey("column"));

        }


      //  [TestMethod]
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

       // [TestMethod]
        public void insert()
        {

            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");

            db.insert("table", "column", "newData");


            Table table = db.Tables["table"];
            Column column = table.Columns["column"];

            Assert.IsTrue(column.list.Contains("newData"));
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
            DataBase db = new DataBase();
            db.load("BD");

            IList<int> lista = new List<int>();
            lista.Add(0);

            IList<int> list = db.where("table", "column", Operator.Equal, "data");

            if(lista[0] == list[0])
            {
                isTrue = true;
            }
            return isTrue;
        }

        public Boolean whereGreater()
        {
            Boolean isTrue = false;
            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");

            IList<int> lista = new List<int>();
            lista.Add(2);
            lista.Add(3);

            IList<int> list = db.where("table", "columnIntNumbers", Operator.Greater, "2");

            if(lista[0] == list[0] && lista[1] == list[1])
            {
                isTrue = true;
            }

            return isTrue;
        }

        public Boolean whereLess()
        {
            Boolean isTrue = false;
            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");

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
            database.createTable("table");

            Table table = database.Tables["table"];
            table.createColumn("column", Library.Type.Text);

            Column column = table.Columns["column"];
            column.list.Add("data");

            database.write();

            DataBase db = new DataBase();

            db.load("BD");

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

        [TestMethod]
        public void delete()
        {

            DataBase database = new DataBase("BD", "admin", "admin");
            database.createTable("table");

            Table table = database.Tables["table"];
            table.createColumn("column", Library.Type.Text);

            Column column = table.Columns["column"];
            column.list.Add("data");

            database.write();

            database.deletefile("BD");

            Assert.IsFalse(File.Exists("BD"));

        }


    }
}
