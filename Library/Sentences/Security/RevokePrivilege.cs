using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Sentences.Security
{
    class RevokePrivilege: SecurityQueries
    {


        public Privilege Type;
        public string Table;

        public RevokePrivilege(Privilege type, string tableName, string securityProfile) : base(securityProfile)
        {
            Type = type;
            Table = tableName;
            SecurityProfileName = securityProfile;
        }


    }
}
