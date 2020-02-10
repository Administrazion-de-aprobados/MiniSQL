﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class User
    {
        public string Name;
        public string Password;
        IList<string> privileges;

        public User (String name, String pass)
        {
            Name = name;
            Password = pass;
            privileges = new List<string>();
        }

    }
}
