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
        
        public void searchColumnName()
        {

        }
        public void deleteColumn()
        {

        }
        public void update()
        {

        }
        public void insert()
        {

        }
    }
}
