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
            
        }

        public void dropTable(string name)
        {

        }

        public void searchColumnName(string name) { 
        
        }

        public void deleteColumn(string name) { 
        
        }

        public void update(string name, string dateToUpdate, Operator op, string tableName) { 
        
        }

        public void insert() { 
        
        }





    }
}
