using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Test
{
  
    public class BDcreation
    {
    
        public static void BDcreatioon()
        {
           
            DataBase database = new DataBase("BD","admin","admin");

            List<string> list = new List<string>();

            list.Add("column TEXT");
            list.Add("columnIntNumbers INT");
            list.Add("columnDoubleNumbers DOUBLE");

            database.createTable("table", list);
            
            Table table = database.Tables["table"];
            //table.createColumn("column", Library.Type.Text);
            //table.createColumn("columnIntNumbers", Library.Type.Int);
            //table.createColumn("columnDoubleNumbers", Library.Type.Double);

            Column column = table.Columns["column"];
            column.list.Add("data");

            Column columnInt = table.Columns["columnIntNumbers"];
            columnInt.list.Add("1");
            columnInt.list.Add("2");
            columnInt.list.Add("3");
            columnInt.list.Add("4");

            Column columnDouble = table.Columns["columnDoubleNumbers"];
            columnDouble.list.Add("1.00");
            columnDouble.list.Add("2.01");
            columnDouble.list.Add("3.10");
            columnDouble.list.Add("4.99");


            List<String> list2 = new List<string>();
            list2.Add("prueba TEXT");

            database.createTable("table2", list2);
            Table table2 = database.Tables["table2"];
            //table2.createColumn("prueba", Library.Type.Text);

            Column columnPrueba = table2.Columns["prueba"];
            columnPrueba.list.Add("data");

            database.write();


           


        }



    }
}
