using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    
    public class Where
    {

        public string colData;
        public string col;
        public Operator op;

        public Where(string column, Operator ope, string data) {

            col = column;
            op = ope;
            colData = data;

        }
    }
}
