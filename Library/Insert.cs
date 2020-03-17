using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Insert: Sentence
    {
        string tipoSentence;
        string tableName;
        List<string> row;
        
        public Insert(string tipo, string table,List<string> newRow) : base(tipo, table)
        {
            tipoSentence = tipo;
            tableName = table;
            row=newRow;
        }
    }
}
