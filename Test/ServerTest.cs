using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using ServerTCP;


namespace Test
{
    [TestClass]
     public class ServerTest
    {
      [TestMethod]

        public void serverParser()
        {
            serverParserOpen();
            serverParserQuery();
            serverParserClose();
        }

        public void serverParserOpen() {
            
            
            string sentenceOpen = "<Open Database=\"Database1\" User=\"admin\" Password=\"admin\"/>";
           
            string respuesta = MyTcpListener.serverParser(sentenceOpen);

            Assert.IsTrue(respuesta.Equals("<Success/>"));

        }

        public void serverParserQuery()
        {

            string sentenceQuery = "<Query>CREATE TABLE EmployeesPersonal (Name TEXT,Age INT,Address TEXT);</Query>";
            
            string respuesta = MyTcpListener.serverParser(sentenceQuery);

            Assert.IsTrue(respuesta.Equals("<Answer>Table created</Answer>") );
            
        }

        
        public void serverParserClose() {
            string sentenceClose = "<Close/>";
            string respuesta = MyTcpListener.serverParser(sentenceClose);
            Assert.IsTrue(respuesta.Equals("<Close/>"));
        }

    }
}
