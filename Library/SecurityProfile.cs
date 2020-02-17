using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class SecurityProfile
    {
        string tableName;
        IList<string> privileges;

        public SecurityProfile(string table)
        {
            tableName = table;
            privileges = new List<string>();
        } 

        public void addPrivileges(string privilege)
        {


        }

        public Boolean searchPrivilege(string privilege)
        {
            return false;
        }

    }
}
