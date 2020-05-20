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
      //  [TestMethod]

        public void serverParser()
        {
            serverParserOpen();
            
        }

        public void serverParserOpen() {

            string sentenceOpen = "<Open Database=\"Database1\" User=\"admin\" Password=\"admin\"/>";
            BDcreation.BDcreatioon();
            DataBase db = DataBase.load("BD");
            string respuesta = MyTcpListener.serverParser(sentenceOpen);

            Assert.IsTrue(sentenceOpen.Equals("<Success>Database created</Success>"));

        }

        }
}
