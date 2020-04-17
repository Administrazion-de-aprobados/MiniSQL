using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Sentences.Security
{
    public class AddUser : SecurityQueries
    {

        public string User;
        public string Password;

        public AddUser(string userName, string pass, string securityProfile) : base(securityProfile)
        {
            User = userName;
            Password = pass;
            SecurityProfileName = securityProfile;
        }


    }
}
