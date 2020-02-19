using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum Operator { Greater, Less, Equal};


    public class DataBase
    {
        public Dictionary<string, User> Users;
        public Dictionary<string, Table> Tables;
        private Admin admin;
        public string Name;

        public DataBase(string name, string adminName, string pass)
        {
            Users = new Dictionary<string, User>();
            Tables = new Dictionary<string, Table>();

            Name = name;
            admin = new Admin(adminName, pass);

        }
        public void createTable(string name)
        {
            Table table = new Table(name);
            Tables.Add(name, table);
        }

        public void dropTable(string name)
        {
            Tables.Remove(name);
        }

        public void searchColumnName(string name) { 
        
        }

        public void deleteColumn(string name) { 
        
        }

        public void update(string tableName, string columnName, string dataToUpdate, Operator op, String valueToCompare ) { 
        
        }

        public void insert(string nameTable, string nameCol, string dataToInsert) { 
        
        }

        public void writte()
        {
            if(File.Exists(Name))
            {
                deletefile();
            }

            StreamWriter sw = File.CreateText(Name);


            sw.WriteLine(admin.Name + "," + admin.Password);

            foreach(KeyValuePair<string, Table> entry in Tables)
            {
                string tableName = entry.Key;
                sw.WriteLine(tableName);

                Table table = entry.Value;
                foreach(KeyValuePair<string, Column> entry2 in table.table)
                {
                    string columnName = entry2.Key;
                    Column column = entry2.Value;
                    string line = columnName + "," + column.columnType + ";";

                    foreach(string value in column.list)
                    {
                         line = line +","+ value;
                    }
                    sw.WriteLine(line);


                }
                sw.WriteLine(" ");

            }
        }

        public void deletefile(){
        
        
        }
    }
}
