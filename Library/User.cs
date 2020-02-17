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
        IList<SecurityProfile> privileges;

        public User (String name, String pass)
        {
            Name = name;
            Password = pass;
            privileges = new List<SecurityProfile>();
        }

        public Boolean searchPrivilegeTable(string table, string privilege)
        {

            return false;
        }



    }
}
