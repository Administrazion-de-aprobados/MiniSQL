﻿using System;
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
            database.createTable("table");
            Table table;

            if (database.Tables.ContainsKey("table"))
            {
                table = database.Tables["table"];
            }




        }



    }
}
