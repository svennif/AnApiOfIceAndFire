using System.Linq;
using AnApiOfIceAndFire.Data.Entities;

namespace AnApiOfIceAndFire.Domain.Characters
{
    public class CharacterFilter : IEntityFilter<CharacterEntity>
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }
        public bool? IsAlive { get; set; }
        public Gender? Gender { get; set; }

        public IQueryable<CharacterEntity> Apply(IQueryable<CharacterEntity> querySet)
        {
            if (querySet == null) return Enumerable.Empty<CharacterEntity>().AsQueryable();

            if (Name != null)
            {
                querySet = querySet.Where(c => c.Name.Equals(Name));
            }
            if (Culture != null)
            {
                querySet = querySet.Where(c => c.Culture.Equals(Culture));
            }
            if (Born != null)
            {
                querySet = querySet.Where(c => c.Born.Equals(Born));
            }
            if (Died != null)
            {
                querySet = querySet.Where(c => c.Died.Equals(Died));
            }
            if (IsAlive.HasValue)
            {
                if (IsAlive.Value)
                {
                    querySet = querySet.Where(c => c.Died.Length == 0);
                }
                else
                {
                    querySet = querySet.Where(c => c.Died.Length > 0);
                }
            }

            if (Gender.HasValue)
            {
                if (Gender.Value == Characters.Gender.Female)
                {
                    querySet = querySet.Where(c => c.IsFemale.HasValue && c.IsFemale.Value);
                }
                else if (Gender.Value == Characters.Gender.Male)
                {
                    querySet = querySet.Where(c => c.IsFemale.HasValue && !c.IsFemale.Value);
                }
                else
                {
                    querySet = querySet.Where(c => !c.IsFemale.HasValue);
                }
            }
            
            return querySet;
        }
    }
}