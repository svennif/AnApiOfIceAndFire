using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Entities;
using Newtonsoft.Json;

namespace AnApiOfIceAndFire.DataFeeder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Time(() =>
            {
                var books = Time(() => JsonConvert.DeserializeObject<List<BookData>>(File.ReadAllText("..\\books.json")), "Loading of book data");
                var characters = Time(() => JsonConvert.DeserializeObject<List<CharacterData>>(File.ReadAllText("..\\characters.json")), "Loading of character data");
                var houses = Time(() => JsonConvert.DeserializeObject<List<HouseData>>(File.ReadAllText("..\\characters.json")), "Loading of house data");

                var mappedBooks = Time(() => MapBooks(books, characters), "Mapping of book data");
                var mappedCharacters = Time(() => MapCharacters(characters), "Mapping of character data");
                var mappedHouses = Time(() => MapHouses(houses, characters), "Mapping of house data");

                //Upsert data

                //Add option to either load data or dump data depending on arguments given

            }, "Total time");

            Console.ReadLine();
        }

        private static List<BookEntity> MapBooks(List<BookData> bookData, List<CharacterData> characterData)
        {
            var bookCharacterMappings = new Dictionary<int, List<int>>();
            var povBookCharacterMappings = new Dictionary<int, List<int>>();
            var books = new List<BookEntity>();

            foreach (var chr in characterData)
            {
                foreach (var book in chr.Books)
                {
                    if (bookCharacterMappings.ContainsKey(book))
                    {
                        var list = bookCharacterMappings[book];
                        list.Add(chr.Id);
                        bookCharacterMappings[book] = list;
                    }
                    else
                    {
                        var list = new List<int> { chr.Id };
                        bookCharacterMappings[book] = list;
                    }
                }
                foreach (var povBook in chr.PovBooks)
                {
                    if (povBookCharacterMappings.ContainsKey(povBook))
                    {
                        var list = povBookCharacterMappings[povBook];
                        list.Add(chr.Id);
                        povBookCharacterMappings[povBook] = list;
                    }
                    else
                    {
                        var list = new List<int> { chr.Id };
                        povBookCharacterMappings[povBook] = list;
                    }
                }
            }

            foreach (var book in bookData)
            {
                var bookEntity = new BookEntity
                {
                    Id = book.Id,
                    Name = book.Name,
                    Authors = book.Authors,
                    NumberOfPages = book.NumberOfPages,
                    Country = book.Country,
                    //ReleaseDate = 
                    ISBN = book.ISBN,
                    MediaType = book.MediaType,
                    Type = "BookEntity",
                    Publisher = book.Publisher,
                    FollowedById = book.FollowedBy,
                    PrecededById = book.PrecededBy
                };

                if (bookCharacterMappings.ContainsKey(bookEntity.Id))
                {
                    bookEntity.Characters = bookCharacterMappings[bookEntity.Id].ToArray();
                }
                if (povBookCharacterMappings.ContainsKey(bookEntity.Id))
                {
                    bookEntity.PovCharacters = povBookCharacterMappings[bookEntity.Id].ToArray();
                }

                books.Add(bookEntity);
            }

            return books;
        }

        private static List<CharacterEntity> MapCharacters(List<CharacterData> characterData)
        {
            var characters = new List<CharacterEntity>();

            foreach (var chrData in characterData)
            {
                var character = new CharacterEntity
                {
                    Id = chrData.Id,
                    Name = chrData.Name,
                    Born = chrData.Born,
                    Died = chrData.Died,
                    Culture = chrData.Culture,
                    Titles = chrData.Titles,
                    Aliases = chrData.Aliases,
                    IsFemale = chrData.IsFemale,
                    Allegiances = chrData.Allegiances,
                    ChildrenIds = chrData.Children,
                    FatherId = chrData.Father,
                    MotherId = chrData.Mother,
                    SpouseId = chrData.Spouse,
                    PlayedBy = chrData.PlayedBy,
                    TvSeries = chrData.TvSeries,
                    Books = chrData.Books,
                    PovBooks = chrData.PovBooks,
                    Type = "CharacterEntity"
                };

                characters.Add(character);
            }

            return characters;
        }

        private static List<HouseEntity> MapHouses(List<HouseData> houseData, List<CharacterData> characterData)
        {
            var houses = new List<HouseEntity>();
            var swornMembersMapping = new Dictionary<int, List<int>>();

            foreach (var chr in characterData)
            {
                foreach (var allegiance in chr.Allegiances)
                {
                    if (swornMembersMapping.ContainsKey(allegiance))
                    {
                        var list = swornMembersMapping[allegiance];
                        list.Add(chr.Id);
                        swornMembersMapping[allegiance] = list;
                    }
                    else
                    {
                        var list = new List<int>() { chr.Id };
                        swornMembersMapping[allegiance] = list;
                    }
                }
            }

            foreach (var house in houseData)
            {
                var houseEntity = new HouseEntity
                {
                    Id = house.Id,
                    Name = house.Name,
                    Region = house.Region,
                    Words = house.Words,
                    Titles = house.Titles,
                    AncestralWeapons = house.AncestralWeapons,
                    Seats = house.Seats,
                    Founded = house.Founded,
                    FounderId = house.Founder,
                    DiedOut = house.DiedOut,
                    CadetBranches = house.CadetBranches,
                    CoatOfArms = house.CoatOfArms,
                    CurrentLordId = house.CurrentLord,
                    HeirId = house.Heir,
                    OverlordId = house.Overlord,
                    Type = "HouseEntity"
                };

                if (swornMembersMapping.ContainsKey(houseEntity.Id))
                {
                    houseEntity.SwornMembers = swornMembersMapping[houseEntity.Id].ToArray();
                }
            }

           return houses;
        }

        private static void Time(Action action, string message)
        {
            var sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();

            Console.WriteLine($"{message}: {sw.ElapsedMilliseconds} milliseconds");
        }

        private static T Time<T>(Func<T> func, string message)
        {
            var sw = new Stopwatch();
            sw.Start();
            var result = func();
            sw.Stop();

            Console.WriteLine($"{message}: {sw.ElapsedMilliseconds} milliseconds");

            return result;
        }
    }
}