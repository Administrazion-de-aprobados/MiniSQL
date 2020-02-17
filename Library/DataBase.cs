using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum Operator { Greater, Less, Equal};
    public class DataBase
    {
        Dictionary<string, User> Users;
        Dictionary<string, Table> Tables;
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





    }
}
