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
            if (querySet == null) return Enumerable.Empty<HouseEntity>().AsQueryable();

            if (Name != null)
            {
                querySet = querySet.Where(h => h.Name.Equals(Name));
            }
            if (Region != null)
            {
                querySet = querySet.Where(h => h.Region.Equals(Region));
            }
            if (Words != null)
            {
                querySet = querySet.Where(h => h.Words.Equals(Words));
            }
            if (HasWords.HasValue)
            {
                if (HasWords.Value)
                {
                    querySet = querySet.Where(h => h.Words.Length > 0);
                }
                else
                {
                    querySet = querySet.Where(h => h.Words.Length == 0);
                }
            }
            if (HasTitles.HasValue)
            {
                if (HasTitles.Value)
                {
                    querySet = querySet.Where(h => h.Titles.Length > 0);
                }
                else
                {
                    querySet = querySet.Where(h => h.Titles.Length == 0);
                }
            }
            if (HasSeats.HasValue)
            {
                if (HasSeats.Value)
                {
                    querySet = querySet.Where(h => h.Seats.Length > 0);
                }
                else
                {
                    querySet = querySet.Where(h => h.Seats.Length == 0);
                }
            }
            if (HasDiedOut.HasValue)
            {
                if (HasDiedOut.Value)
                {
                    querySet = querySet.Where(h => h.DiedOut.Length > 0);
                }
                else
                {
                    querySet = querySet.Where(h => h.DiedOut.Length == 0);
                }
            }
            if (HasAncestralWeapons.HasValue)
            {
                if (HasAncestralWeapons.Value)
                {
                    querySet = querySet.Where(h => h.AncestralWeapons.Length > 0);
                }
                else
                {
                    querySet = querySet.Where(h => h.AncestralWeapons.Length == 0);
                }
            }

            return querySet;
        }
    }
}