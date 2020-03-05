using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;

namespace Test
{
    [TestClass]
    public class AdminTest
    {

        [TestMethod]
        public void testCreateSecurityProfile()
        {
            String table="tablaPrueba";
            String name = "JUAN";
            String pass = "123456789";

            Admin admin = new Admin(name,pass);

            

           
        }

        [TestMethod]
        public void testdropSecurityProfile()
        {

        }
        [TestMethod]
        public void testgrant()
        {

        }
        [TestMethod]
        public void testrevoke()
        {

        }
        [TestMethod]
        public void testaddUser()
        {

        }
        [TestMethod]
        public void testdeleteUser()
        {

        }


    }
}
