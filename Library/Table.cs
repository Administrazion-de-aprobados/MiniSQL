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
        
        public void addToTable(string name, Type type, List<string> columnValues) {

            Column column;

            if (!Columns.ContainsKey(name))
            {
                column = new Column(name, type);
                Columns.Add(name, column);

            }
            else {
                column = Columns[name];
            }
            column.addColumns(columnValues);

            }
        public void createColumn(string name, Type type) {

            if (!Columns.ContainsKey(name))
            {
                Column column = new Column(name, type);
                Columns.Add(name, column);

            }

        }

        public string selectToString() {


            string line1 = "[";

            int numLines=0;
            foreach (Column col in Columns.Values ) {

                line1 = line1 + "'" + col.Name + "'" + ",";
                numLines=col.list.Count; 
            }
            
            line1=line1.TrimEnd(',');
            line1 = line1 + "]";

            if (numLines!=0) {
                for (int i = 0; i < numLines; i++) {

                    string list = "{";
                    foreach (Column col in Columns.Values) {

                        list = list + "'" + col.list[i]  + "'" + ",";

                    }

                    list = list.TrimEnd(',');
                    list = list + "}";
                    line1 = line1 + " " + list;

                }
            }
            return line1;
        }
    }
}
