using System;
using System.Collections.Generic;

namespace AnApiOfIceAndFire.Models.v1
{
    public class Organisation
    {
        public string URL { get; }
        public string Name { get; }
        public string Description { get; set; }
        public string Founded { get; set; }
        public string Founder { get; set; }
        public IEnumerable<string> KnownMembers { get; set; }

        public Organisation(string url, string name)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));
            if (name == null) throw new ArgumentNullException(nameof(name));
            URL = url;
            Name = name;
        }
    }
}