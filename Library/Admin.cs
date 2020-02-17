using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
   public class Admin
    {
        public string Name;
        public string Password;

        public Admin(string name, string pass)
        {
            Name = name;
            Password = pass;
            List<SecurityProfile> securityProfiles = new List<SecurityProfile>();
        }

        public void createSecurityProfile(string tableName) {

        }

        public void dropSecurityProfile(string name) {

        }

        public void grant(string privilege, string table, string securityProf) { 
        
        }
        public void revoke(string privilege, string table, string securityProf) { 
        
        }

        public void addUser(string user, string password, string securityProf) { 
        
        }

        public void deleteUser(string user) { 
        
        }

       


    }
}
