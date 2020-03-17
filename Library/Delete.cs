using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library
{
    public class Delete : Sentence
    {
        string tipoSentence;
        string tableName;
        Where sentenceWhere;
      
        public Delete(string tipo, string table, Where where) : base(tipo, table)
        {

            tipoSentence = tipo;
            tableName = table;
            sentenceWhere = where;
          
        }

    }
}
