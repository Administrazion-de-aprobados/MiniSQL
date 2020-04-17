using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Select : Statements
    {
        public Where sentenceWhere;
        public List<string> listColumns;

        public Select(string table , List<string> columns, Where where) : base(table)
        {

            tableName = table;
            sentenceWhere = where;
            listColumns = columns;


        }

    }
}
