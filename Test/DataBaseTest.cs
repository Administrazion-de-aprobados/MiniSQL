using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        }

        [TestMethod]
        public void searchColumnName()
        {

        }

        [TestMethod]
        public void deleteColumn()
        {

        }

        [TestMethod]
        public void update()
        {

        }

        [TestMethod]
        public void insert()
        {

        }
    }
}
