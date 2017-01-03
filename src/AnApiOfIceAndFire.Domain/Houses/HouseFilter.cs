using System.Linq;
using AnApiOfIceAndFire.Data.Entities;

namespace AnApiOfIceAndFire.Domain.Houses
{
    public class HouseFilter : IEntityFilter<HouseEntity>
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Words { get; set; }
        public bool? HasWords { get; set; }
        public bool? HasTitles { get; set; }
        public bool? HasSeats { get; set; }
        public bool? HasDiedOut { get; set; }
        public bool? HasAncestralWeapons { get; set; }

        public IQueryable<HouseEntity> Apply(IQueryable<HouseEntity> querySet)
        {
            throw new System.NotImplementedException();
        }
    }
}