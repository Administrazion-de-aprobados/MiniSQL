using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Update: Sentence
    {
        string tipoSentence;
        string tableName;
        Where sentenceWhere;
        List<string> column;
        List<string> newValue;

        public Update(string tipo, string table, List<string> col, List<string> newData, Where where) : base(tipo, table)
        {
            tipoSentence = tipo;
            tableName = table;
            sentenceWhere = where;
            column = col;
            newValue = newData;

        }
    }
}
