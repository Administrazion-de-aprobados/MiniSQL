using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Insert: Statements
    {
        public List<string> row;
        
        public Insert(string table,List<string> newRow) : base(table)
        {
            tableName = table;
            row=newRow;
        }
    }
}
