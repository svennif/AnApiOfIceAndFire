namespace AnApiOfIceAndFire.Domain.Models.Filters
{
    public class OrganisationFilter
    {
        public string Name { get; set; }
        public string Founded { get; set; }
        public string Founder { get; set; }
        public bool? HasKnownMembers { get; set; }
    }
}