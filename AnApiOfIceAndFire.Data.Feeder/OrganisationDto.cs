using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnApiOfIceAndFire.Data.Feeder
{
    public class OrganisationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Founded { get; set; }
        public int? Founder { get; set; }
        public int[] Books { get; set; }
    }
}