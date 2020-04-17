using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class SecurityProfile
    {
        public string Name;
        public Dictionary<string, List<Library.Privilege>> Privileges;

        public SecurityProfile(string secName)
        {
            Name = secName;
            Privileges = new Dictionary<string, List<Privilege>>();
        } 

    }
}
