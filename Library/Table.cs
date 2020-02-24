using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Table
    {
        public string Name;

        public Dictionary<string, Column> table;

        public Table(string name)
        {
            Name = name;
            table = new Dictionary<string, Column>();

        }
        public void searchColumnName(string name)
        {

        }
        public void addToColumn() { 
            
        }
        public void createColumn(string name) {
            
            Column column = new Column();
            table.Add(name, column);

        }
        public void deleteColumn(string name)
        {

        }
        public void update(string columnName, string dataToUpdate, Operator op, String valueToCompare)
        {

        }
        public void insert(string columnName, string data)
        {

        }
    }
}
