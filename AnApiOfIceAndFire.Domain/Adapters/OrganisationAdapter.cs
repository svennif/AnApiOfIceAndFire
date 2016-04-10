using System;
using System.Collections.Generic;
using System.Linq;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Models;

namespace AnApiOfIceAndFire.Domain.Adapters
{
    public class OrganisationAdapter : IOrganisation
    {
        private readonly OrganisationEntity _entity;

        public int Identifier => _entity.Id;
        public string Name => _entity.Name;
        public string Description => _entity.Description;
        public string Founded => _entity.Founded;

        private ICharacter _founder;
        public ICharacter Founder
        {
            get
            {
                if (_entity.Founder == null)
                {
                    return null;
                }

                return _founder ?? (_founder = new CharacterEntityAdapter(_entity.Founder));
            }
        }

        private IReadOnlyCollection<IBook> _books;
        public IReadOnlyCollection<IBook> Books
        {
            get
            {
                return _books ?? (_books = _entity.Books.Select(c => new BookEntityAdapter(c)).ToList());
            }
        }

        private IReadOnlyCollection<ICharacter> _members;
        public IReadOnlyCollection<ICharacter> Members
        {
            get { return _members ?? (_members = _entity.Members.Select(x => new CharacterEntityAdapter(x)).ToList()); }
        }

        public OrganisationAdapter(OrganisationEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _entity = entity;
        }
    }
}