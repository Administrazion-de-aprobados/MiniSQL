using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Insert: Sentence
    {
        public string tipoSentence;
        public string tableName;
        public List<string> row;
        
        public Insert(string table,List<string> newRow) : base(table)
        {
            tipoSentence = "INSERT";
            tableName = table;
            row=newRow;
        }
    }
}
