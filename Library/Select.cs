using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Select : Sentence
    {
        string tipoSentence;
        string tableName;
        Where sentenceWhere;
        List<string> listColumns;
        public Select(string tipo, string table , Where where,List<string> columns) : base(tipo, table)
        {

            tipoSentence = tipo;
            tableName = table;
            sentenceWhere = where;
            listColumns = columns;



        }

    }
}
