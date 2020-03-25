using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;
using System.IO;


namespace Test
{

    [TestClass]
     public class MiniSQLTest
    {

        [TestMethod]
        public void tester()
        {


            MiniSQL.tester("C:\\Users\\yeray\\Desktop\\input-file.txt", "C:\\Users\\yeray\\Desktop\\output-file.txt");

            Assert.IsTrue(true);

        }






    }

}
