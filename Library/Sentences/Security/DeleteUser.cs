using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Sentences.Security
{
    public class DeleteUser : SecurityQueries
    {

        string User;

        public DeleteUser(string user, string name = null) : base (name)  
        {
            User = user;
        }



    }
}
