using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class CreateTable: Statements
    {

        public List<String> ListOfColumns;

        public CreateTable(string table, List<string> listOfColumns) : base(table)
        {

            tableName = table;
            ListOfColumns = listOfColumns;

        }




    }
}
