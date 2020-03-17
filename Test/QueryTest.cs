using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;
using System.IO;
using Library;

namespace Test
{
    //[TestClass]
    class QueryTest
    { 
        //[TestMethod]
        public void parse()
        {
            BDcreation.BDcreatioon();
            DataBase db = new DataBase();
            db.load("BD");


        }

        public Boolean parseSelect()
        {

            //Select select = Query.parse("SELECT column FROM table WHERE 1>1");

            


            return false;
        }


    }
}
