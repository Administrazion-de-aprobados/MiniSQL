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
        public void searchColumnName(string name)
        {

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
