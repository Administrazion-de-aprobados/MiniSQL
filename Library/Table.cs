using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Table
    {
        public string Name;

        Dictionary<string, Column> tabla;

        public Table(string name)
        {
            Name = name;
            tabla = new Dictionary<string, Column>();

        }
        public void searchColumnName(string name)
        {

        }
        public void deleteColumn(string name)
        {

        }
        public void update(string name, string dateToUpdate, Operator op)
        {

        }
        public void insert(string columnName, string data)
        {

        }
    }
}
