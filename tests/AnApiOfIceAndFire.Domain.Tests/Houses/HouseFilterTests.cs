using System.Collections.Generic;
using System.Linq;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Houses;
using Xunit;

namespace AnApiOfIceAndFire.Domain.Tests.Houses
{
    public class HouseFilterTests
    {
        [Fact]
        public void NullQueryable_WhenApplying_ThenEmptyQueryableIsReturned()
        {
            var filter = new HouseFilter();

            var filteredHouses = filter.Apply(null).ToList();

            Assert.Equal(0, filteredHouses.Count);
        }

        [Fact]
        public void NoPropertiesSetOnFilter_WhenApplying_ThenNoHousesAreFiltered()
        {
            var filter = new HouseFilter();
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark"},
                new HouseEntity {Id = 2, Name = "House Tully"}
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(houses.Count, filteredHouses.Count);
        }

        [Fact]
        public void NameSet_WhenApplying_ThenHousesWithDifferentNameAreFiltered()
        {
            var filter = new HouseFilter
            {
                Name = "House Stark"
            };
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark"},
                new HouseEntity {Id = 2, Name = "House Tully"}
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(1, filteredHouses.Count);
            Assert.Equal(houses[0].Name, filteredHouses[0].Name);
        }

        [Fact]
        public void RegionSet_WhenApplying_ThenHousesWithDifferentRegionsAreFiltered()
        {
            var filter = new HouseFilter
            {
                Region = "The North"
            };
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark",Region = "The North"},
                new HouseEntity {Id = 2, Name = "House Tully", Region = "The Riverlands"}
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(1, filteredHouses.Count);
            Assert.Equal(houses[0].Region, filteredHouses[0].Region);
        }

        [Fact]
        public void WordsSet_WhenApplying_ThenHousesWithDifferentWordsAreFiltered()
        {
            var filter = new HouseFilter
            {
                Words = "Winter is coming"
            };
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark", Words = "Winter is coming"},
                new HouseEntity {Id = 2, Name = "House Tully"}
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(1, filteredHouses.Count);
            Assert.Equal(houses[0].Words, filteredHouses[0].Words);
        }

        [Fact]
        public void HasWordsSetToTrue_WhenApplying_ThenHousesWithoutWordsAreFiltered()
        {
            var filter = new HouseFilter
            {
                HasWords = true
            };
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark",Words = "Winter is coming"},
                new HouseEntity {Id = 2, Name = "House Tully"}
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(1, filteredHouses.Count);
            Assert.False(string.IsNullOrEmpty(filteredHouses[0].Words));
        }

        [Fact]
        public void HasWordsSetToFalse_WhenApplying_ThenHousesWithWordsAreFiltered()
        {
            var filter = new HouseFilter
            {
                HasWords = false
            };
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark", Words = "Winter is coming"},
                new HouseEntity {Id = 2, Name = "House Tully"}
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(1, filteredHouses.Count);
            Assert.True(string.IsNullOrEmpty(filteredHouses[0].Words));
        }

        [Fact]
        public void HasTitlesSetToTrue_WhenApplying_ThenHousesWithoutTitlesAreFiltered()
        {
            var filter = new HouseFilter
            {
                HasTitles = true
            };
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark",Titles = new [] {"King of Winter"}},
                new HouseEntity {Id = 2, Name = "House Tully"}
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(1, filteredHouses.Count);
            Assert.True(filteredHouses[0].Titles.Length > 0);
        }

        [Fact]
        public void HasTitlesSetToFalse_WhenApplying_ThenHousesWithTitlesAreFiltered()
        {
            var filter = new HouseFilter
            {
                HasTitles = false
            };
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark",Titles = new [] {"King of Winter"}},
                new HouseEntity {Id = 2, Name = "House Tully"}
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(1, filteredHouses.Count);
            Assert.Equal(0, filteredHouses[0].Titles.Length);
        }

        [Fact]
        public void HasSeatsSetToTrue_WhenApplying_ThenHousesWithoutSeatsAreFiltered()
        {
            var filter = new HouseFilter
            {
                HasSeats = true
            };
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark", Seats = new [] {"Winterfell"}},
                new HouseEntity {Id = 2, Name = "House Tully"}
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(1, filteredHouses.Count);
            Assert.True(filteredHouses[0].Seats.Length > 0);
        }

        [Fact]
        public void HasSeatsSetToFalse_WhenApplying_ThenHousesWithSeatsAreFiltered()
        {
            var filter = new HouseFilter
            {
                HasSeats = false
            };
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark",Seats = new [] {"Winterfell"}},
                new HouseEntity {Id = 2, Name = "House Tully" }
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(1, filteredHouses.Count);
            Assert.Equal(0, filteredHouses[0].Seats.Length);
        }

        [Fact]
        public void HasDiedOutSetToTrue_WhenApplying_ThenHousesThatAreNotDiedOutAreFiltered()
        {
            var filter = new HouseFilter
            {
                HasDiedOut = true
            };
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark", Seats = new [] {"Winterfell"}},
                new HouseEntity {Id = 2, Name = "House X", DiedOut = "200 AC"}
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(1, filteredHouses.Count);
            Assert.False(string.IsNullOrEmpty(filteredHouses[0].DiedOut));
        }

        [Fact]
        public void HasDiedOutSetToFalse_WhenApplying_ThenHousesThatAreDiedOutAreFiltered()
        {
            var filter = new HouseFilter
            {
                HasDiedOut = false
            };
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark",Seats = new [] {"Winterfell"}},
                new HouseEntity {Id = 2, Name = "House X", DiedOut = "200 AC"}
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(1, filteredHouses.Count);
            Assert.True(string.IsNullOrEmpty(filteredHouses[0].DiedOut));
        }

        [Fact]
        public void HasAncestralWeaponsSetToTrue_WhenApplying_ThenHousesWithoutAncestralWeaponsAreFiltered()
        {
            var filter = new HouseFilter
            {
                HasAncestralWeapons = true
            };
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark", AncestralWeapons = new [] {"Ice"}},
                new HouseEntity {Id = 2, Name = "House X", DiedOut = "200 AC"}
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(1, filteredHouses.Count);
            Assert.True(filteredHouses[0].AncestralWeapons.Length > 0);
        }

        [Fact]
        public void HasAncestralWeaponsSetToFalse_WhenApplying_ThenHousesWithAncestralWeaponsAreFiltered()
        {
            var filter = new HouseFilter
            {
                HasAncestralWeapons = false
            };
            var houses = new List<HouseEntity>
            {
                new HouseEntity {Id = 1, Name = "House Stark", AncestralWeapons = new [] {"Ice"}},
                new HouseEntity {Id = 2, Name = "House X", DiedOut = "200 AC"}
            };

            var filteredHouses = filter.Apply(houses.AsQueryable()).ToList();

            Assert.Equal(1, filteredHouses.Count);
            Assert.Equal(0, filteredHouses[0].AncestralWeapons.Length);
        }
    }
}