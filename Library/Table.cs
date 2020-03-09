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

        public Dictionary<string, Column> Columns;

        public Table(string name)
        {
            Name = name;
            Columns = new Dictionary<string, Column>();

        }
        
        public void addToTable(string name, Type type, List<String> columnValues) {

            Column column = new Column(name, type);
            column.addColumns(columnValues);
            Columns.Add(name,column);
           
            
        }
        public void createColumn(string name, Type type) {
            
            Column column = new Column(name, type);
            Columns.Add(name, column);

        }
    }
}
