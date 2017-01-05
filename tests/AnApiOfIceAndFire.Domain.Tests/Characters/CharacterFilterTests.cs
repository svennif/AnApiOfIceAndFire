using System.Collections.Generic;
using System.Linq;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Characters;
using Xunit;

namespace AnApiOfIceAndFire.Domain.Tests.Characters
{
    public class CharacterFilterTests
    {
        [Fact]
        public void NullQueryable_WhenApplying_ThenEmptyQueryableIsReturned()
        {
            var filter = new CharacterFilter();

            var filteredCharacters = filter.Apply(null).ToList();

            Assert.Equal(0, filteredCharacters.Count);
        }

        [Fact]
        public void NoPropertiesSetOnFilter_WhenApplying_ThenNoCharactersAreFiltered()
        {
            var filter = new CharacterFilter();
            var characters = new List<CharacterEntity>
            {
                new CharacterEntity {Id = 1, Name = "Jon Snow"},
                new CharacterEntity {Id = 2, Name = "Eddard Stark"}
            };

            var filteredCharacters = filter.Apply(characters.AsQueryable());

            Assert.Equal(characters.Count, filteredCharacters.Count());
        }

        [Fact]
        public void NameSet_WhenApplying_ThenCharactersWithDifferentNameAreFiltered()
        {
            var filter = new CharacterFilter
            {
                Name = "Jon Snow"
            };
            var characters = new List<CharacterEntity>
            {
                new CharacterEntity {Id = 1, Name = "Jon Snow"},
                new CharacterEntity {Id = 2, Name = "Eddard Stark"}
            };

            var filteredCharacters = filter.Apply(characters.AsQueryable()).ToList();

            Assert.Equal(1, filteredCharacters.Count);
            Assert.Equal(characters[0].Name, filteredCharacters[0].Name);
        }

        [Fact]
        public void CultureSet_WhenApplying_ThenCharactersWithDifferentCultureAreFiltered()
        {
            var filter = new CharacterFilter
            {
                Culture = "Northerner"
            };
            var characters = new List<CharacterEntity>
            {
                new CharacterEntity {Id = 1, Name = "", Culture = ""},
                new CharacterEntity {Id = 2, Name = "Eddard Stark", Culture = "Northerner"}
            };

            var filteredCharacters = filter.Apply(characters.AsQueryable()).ToList();

            Assert.Equal(1, filteredCharacters.Count);
            Assert.Equal(characters[1].Culture, filteredCharacters[0].Culture);
        }

        [Fact]
        public void BornSet_WhenApplying_ThenCharactersWithDifferentBirthdateAreFiltered()
        {
            var filter = new CharacterFilter
            {
                Born = "10 AC"
            };
            var characters = new List<CharacterEntity>
            {
                new CharacterEntity {Id = 1, Name = "", Culture = "", Born = "10 AC"},
                new CharacterEntity {Id = 2, Name = "Jon Snow", Culture = "Northerner", Born = "283 AC"}
            };

            var filteredCharacters = filter.Apply(characters.AsQueryable()).ToList();

            Assert.Equal(1, filteredCharacters.Count);
            Assert.Equal(characters[0].Born, filteredCharacters[0].Born);
        }

        [Fact]
        public void DiedSet_WhenApplying_ThenCharactersWithDifferentDeathAreFiltered()
        {
            var filter = new CharacterFilter
            {
                Died = "20 AC"
            };
            var characters = new List<CharacterEntity>
            {
                new CharacterEntity {Id = 1, Name = "", Culture = "", Died = "20 AC"},
                new CharacterEntity {Id = 2, Name = "Jon Snow", Culture = "Northerner", Born = "283 AC"}
            };

            var filteredCharacters = filter.Apply(characters.AsQueryable()).ToList();

            Assert.Equal(1, filteredCharacters.Count);
            Assert.Equal(characters[0].Died, filteredCharacters[0].Died);
        }

        [Fact]
        public void AliveSetToTrue_WhenAppying_ThenDeadCharactersAreFiltered()
        {
            var filter = new CharacterFilter
            {
                IsAlive = true
            };

            var characters = new List<CharacterEntity>
            {
                new CharacterEntity {Id = 1, Name = "", Culture = "", Died = "20 AC"},
                new CharacterEntity {Id = 2, Name = "Jon Snow", Culture = "Northerner", Born = "283 AC"}
            };

            var filteredCharacters = filter.Apply(characters.AsQueryable()).ToList();

            Assert.Equal(1, filteredCharacters.Count);
            Assert.Equal("Jon Snow", filteredCharacters[0].Name);
        }

        [Fact]
        public void AliveSetToFalse_WhenApplying_ThenAliveCharactersAreFiltered()
        {
            var filter = new CharacterFilter
            {
                IsAlive = false
            };

            var characters = new List<CharacterEntity>
            {
                new CharacterEntity {Id = 1, Name = "", Culture = "", Died = "20 AC"},
                new CharacterEntity {Id = 2, Name = "Jon Snow", Culture = "Northerner", Born = "283 AC"}
            };

            var filteredCharacters = filter.Apply(characters.AsQueryable()).ToList();

            Assert.Equal(1, filteredCharacters.Count);
            Assert.Equal("", filteredCharacters[0].Name);
        }

        [Fact]
        public void GenderSetToFemale_WhenApplying_ThenNonFemaleCharactersAreFiltered()
        {
            var filter = new CharacterFilter
            {
               Gender = Gender.Female
            };

            var characters = new List<CharacterEntity>
            {
                new CharacterEntity {Id = 1, Name = "Jon Snow", IsFemale = false},
                new CharacterEntity {Id = 2, Name = "Arya Stark", IsFemale = true},
                new CharacterEntity {Id = 3, Name = "", IsFemale = null}
            };

            var filteredCharacters = filter.Apply(characters.AsQueryable()).ToList();

            Assert.Equal(1, filteredCharacters.Count);
            Assert.True(filteredCharacters[0].IsFemale);
        }

        [Fact]
        public void GenderSetToMale_WhenApplying_ThenNonMaleCharactersAreFiltered()
        {
            var filter = new CharacterFilter
            {
                Gender = Gender.Male
            };

            var characters = new List<CharacterEntity>
            {
                new CharacterEntity {Id = 1, Name = "Jon Snow", IsFemale = false},
                new CharacterEntity {Id = 2, Name = "Arya Stark", IsFemale = true},
                new CharacterEntity {Id = 3, Name = "", IsFemale = null}
            };

            var filteredCharacters = filter.Apply(characters.AsQueryable()).ToList();

            Assert.Equal(1, filteredCharacters.Count);
            var isFemale = filteredCharacters[0].IsFemale;
            Assert.False(isFemale != null && isFemale.Value);
        }

        [Fact]
        public void GenderSetToUnknown_WhenApplying_ThenNonUnknownGenderedCharactersAreFiltered()
        {
            var filter = new CharacterFilter
            {
                Gender = Gender.Unknown
            };

            var characters = new List<CharacterEntity>
            {
                new CharacterEntity {Id = 1, Name = "Jon Snow", IsFemale = false},
                new CharacterEntity {Id = 2, Name = "Arya Stark", IsFemale = true},
                new CharacterEntity {Id = 3, Name = "", IsFemale = null}
            };

            var filteredCharacters = filter.Apply(characters.AsQueryable()).ToList();

            Assert.Equal(1, filteredCharacters.Count);
            Assert.Null(filteredCharacters[0].IsFemale);
        }
    }
}