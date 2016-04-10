using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Entities;

namespace AnApiOfIceAndFire.Data.Configurations
{
    public class OrganisationConfiguration : EntityTypeConfiguration<OrganisationEntity>
    {
        public OrganisationConfiguration()
        {
            HasOptional(h => h.Founder)
               .WithMany()
               .HasForeignKey(h => h.FounderId);

            //HasMany(h => h.CadetBranches)
            //.WithMany()
            //.Map(hh =>
            //{
            //    hh.MapLeftKey("HouseId1");
            //    hh.MapRightKey("HouseId2");
            //    hh.ToTable("HouseCadetBranches");
            //});

           // HasMany(c => c.Allegiances)
           //.WithMany(h => h.SwornMembers)
           //.Map(x =>
           //{
           //    x.MapLeftKey("CharacterId");
           //    x.MapRightKey("HouseId");
           //    x.ToTable("CharacterHouseAllegiances");
           //});


        }
    }
}


//public CharacterEntity Founder { get; set; }

//public ICollection<CharacterEntity> Members { get; set; } = new List<CharacterEntity>();
//public ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();