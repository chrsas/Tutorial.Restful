using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial.Restful.Configurations
{
    public class FirstConfig
    {
        public string Key1 { get; set; }

        public string Key2 { get; set; }

        public Key3Options Key3 { get; set; }

        public class Key3Options
        {
            public string ChildKey1 { get; set; }
        }
    }
}
