using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library
{
    public class Delete : Sentence
    {
        public Where sentenceWhere;
      
        public Delete( string table, Where where) : base( table)
        {
            tableName = table;
            sentenceWhere = where;
          
        }

    }
}
