using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Column
    {
        public IList<string> list;
        public Type ColumnType;
        public string Name;

        public Column(string name, Type columnType)
        {
            list = new List<string>();
            ColumnType = columnType;
            Name = name;
        }

        public void addColumns(List<String> columnValues) {
            foreach (string value in columnValues) {
                list.Add(value);
                }
        }


        public void deleteData( int i) {
            list.RemoveAt(i);
        }


        public void insert(string data ) {

            list.Add(data);
        
        }

    }
}
