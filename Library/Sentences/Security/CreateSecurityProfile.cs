using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Sentences.Security
{
    public class CreateSecurityProfile : SecurityQueries
    {

        public CreateSecurityProfile(string securityProfile) : base(securityProfile)
        {
            SecurityProfileName = securityProfile;
        }
    }
}
