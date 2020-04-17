using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Sentences.Security
{
    class GrantPrivilege: SecurityQueries
    {

        public Privilege Type;
        public string Table;

        public GrantPrivilege(Privilege type, string tableName, string securityProfile) : base(securityProfile)
        {
            Type = type;
            Table = tableName;
            SecurityProfileName = securityProfile;
        }

    }
}
