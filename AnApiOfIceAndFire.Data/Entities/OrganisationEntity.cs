using System.Collections.Generic;

namespace AnApiOfIceAndFire.Data.Entities
{
    public class OrganisationEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Founded { get; set; }
        public int? FounderId { get; set; }
        public CharacterEntity Founder { get; set; }

        public ICollection<CharacterEntity> Members { get; set; } = new List<CharacterEntity>();
        public ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();  
    }
}