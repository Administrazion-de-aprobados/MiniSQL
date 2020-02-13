using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
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
            
        }

        public void dropTable(string name)
        {

        }

        public void searchColumnName() { 
        
        }

        public void deleteColumn() { 
        
        }

        public void update() { 
        
        }

        public void insert() { 
        
        }





    }
}
