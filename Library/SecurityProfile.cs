using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class SecurityProfile
    {
        string Name;
        Dictionary<string, List<Library.Privilege>> Privileges;

        public SecurityProfile(string secName)
        {
            Name = secName;
            Privileges = new Dictionary<string, List<Privilege>>();
        } 

    }
}
