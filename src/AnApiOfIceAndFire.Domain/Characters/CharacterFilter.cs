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
            throw new System.NotImplementedException();
        }
    }
}