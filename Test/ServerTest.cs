using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using ServerTCP;
using System.IO;

namespace Test
{
    [TestClass]
     public class ServerTest
    {
      [TestMethod]

        public void serverParser()
        {
            if (File.Exists("Database1"))
                File.Delete("Database1");
            serverParserOpen();
            serverParserQuery();
            serverParserClose();
        }

        public void serverParserOpen() {

            //BDcreation.BDcreatioon();

            string sentenceOpen = "<Open Database=\"Database1\" User=\"admin\" Password=\"admin\"/>";

            string respuesta = MyTcpListener.serverParser(sentenceOpen);
            MyTcpListener.serverParser("<Close/>");

            if (respuesta.Equals("<Success/>"))
                Assert.IsTrue(true);
            else if(respuesta.Equals("<Success>" + Constants.CreateDatabaseSuccess + "</Success>"))
                Assert.IsTrue(true);
            else
                Assert.IsTrue(false);


        }

        public void serverParserQuery()
        {
            string sentenceOpen = "<Open Database=\"Database1\" User=\"admin\" Password=\"admin\"/>";

            MyTcpListener.serverParser(sentenceOpen);

            string sentenceQuery = "<Query>CREATE TABLE EmployeesPersonal (Name TEXT,Age INT,Address TEXT);</Query>";
            
            string respuesta = MyTcpListener.serverParser(sentenceQuery);

            MyTcpListener.serverParser("<Close/>");
            Assert.IsTrue(respuesta.Equals("<Answer>Table created</Answer>") );
            
        }

        
        public void serverParserClose() {
            string sentenceOpen = "<Open Database=\"Database1\" User=\"admin\" Password=\"admin\"/>";

            MyTcpListener.serverParser(sentenceOpen);
            string sentenceClose = "<Close/>";
            string respuesta = MyTcpListener.serverParser(sentenceClose);
            Assert.IsTrue(respuesta.Equals("<Close/>"));
        }

    }
}
