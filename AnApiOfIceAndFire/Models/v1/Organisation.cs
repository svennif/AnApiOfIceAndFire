using System.Collections.Generic;

namespace AnApiOfIceAndFire.Models.v1
{
    public class Organisation
    {
        public string URL { get; }
        public string Name { get; }
        public string Description { get; }
        public string Founded { get; }
        public string Founder { get; }
        public IEnumerable<string> KnownMembers { get; } 
    }
}