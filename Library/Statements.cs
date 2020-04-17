using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Statements : Sentence
    {
        public string tableName;

        public Statements(string table)
        {
            tableName = table;
        }
    }
}