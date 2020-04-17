using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class SelectAll : Statements
    {

        public List<string> listColumns;

        public SelectAll(string table, List<string> columns) : base(table)
        {

            tableName = table;
            
            listColumns = columns;


        }


    }
}
