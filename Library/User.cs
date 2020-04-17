using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class User
    {
        public string Name;
        public string Password;
        public List<string> SecurityProfiles;

        public User (String name, String pass)
        {
            Name = name;
            Password = pass;
            SecurityProfiles = new List<string>();
        }

        public Boolean searchPrivilegeTable(string table, string privilege)
        {

            return false;
        }


    }
}
