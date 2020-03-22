using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Update: Sentence
    {
        public Where sentenceWhere;
        public List<string> column;
        public List<string> newValue;

        public Update(string table, List<string> col, List<string> newData, Where where) : base(table)
        {
            tableName = table;
            sentenceWhere = where;
            column = col;
            newValue = newData;

        }
    }
}
