using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    
    public class Where
    {

        string colData;
        string col;
        Operator op;

        public Where(string column, Operator ope, string data) {

            col = column;
            op = ope;
            colData = data;

        }
    }
}
