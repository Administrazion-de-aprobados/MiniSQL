using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;
using System.IO;


namespace Test
{

//[TestClass]
     public class MiniSQLTest
    {

        //[TestMethod]
        public void tester()
        {


            //MiniSQL.tester("input-file.txt", "output-file.txt");
           MiniSQL.tester("C:\\Users\\yeray\\Desktop\\goodInput.txt", "C:\\Users\\yeray\\Desktop\\output03.txt");
            Assert.IsTrue(true);

        }






    }

}
