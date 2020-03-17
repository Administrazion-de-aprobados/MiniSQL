using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library
{
    public class Delete : Sentence
    {
        public string tipoSentence;
        public string tableName;
        public Where sentenceWhere;
      
        public Delete( string table, Where where) : base( table)
        {

            tipoSentence = "DELETE";
            tableName = table;
            sentenceWhere = where;
          
        }

    }
}
