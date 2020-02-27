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

        public void searchData(Operator op) { 
        
            
        }

        public void addColumns(List<String> columnValues) {
            foreach (string value in columnValues) {
                list.Add(value);
                }
        }


        public void deleteData(string data) { 
        
        }

        public void updateData(string newdata, Operator op, string dataToChange) { 
            
        }

        public void insert(string data ) { 
        
        }

    }
}
