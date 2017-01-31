using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnApiOfIceAndFire.Data.Entities;
using AutoMapper;
using Newtonsoft.Json;

namespace AnApiOfIceAndFire.DataFeeder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BookData, BookEntity>()
                    .ForMember(character => character.Type, opt => opt.UseValue("BookEntity"))
                    .ForMember(book => book.Characters, opt => opt.Ignore())
                    .ForMember(book => book.PovCharacters, opt => opt.Ignore())
                    .ForMember(book => book.ReleaseDate, opt => opt.Ignore());

                cfg.CreateMap<CharacterData, CharacterEntity>()
                    .ForMember(character => character.Type, opt => opt.UseValue("CharacterEntity"));

                cfg.CreateMap<HouseData, HouseEntity>()
                    .ForMember(house => house.Type, opt => opt.UseValue("HouseEntity"))
                    .ForMember(house => house.SwornMembers, opt => opt.Ignore());
            });

            Time(() =>
            {
                var books = Time(() => JsonConvert.DeserializeObject<List<BookData>>(File.ReadAllText("..\\books.json")), "Loading of book data");
                var characters = Time(() => JsonConvert.DeserializeObject<List<CharacterData>>(File.ReadAllText("..\\characters.json")), "Loading of character data");
                var houses = Time(() => JsonConvert.DeserializeObject<List<HouseData>>(File.ReadAllText("..\\houses.json")), "Loading of house data");

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
                var bookEntity = Mapper.Map<BookEntity>(book);

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
            return characterData.Select(Mapper.Map<CharacterEntity>).ToList();
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
                        var list = new List<int> { chr.Id };
                        swornMembersMapping[allegiance] = list;
                    }
                }
            }

            foreach (var house in houseData)
            {
                var houseEntity = Mapper.Map<HouseEntity>(house);
                if (swornMembersMapping.ContainsKey(houseEntity.Id))
                {
                    houseEntity.SwornMembers = swornMembersMapping[houseEntity.Id].ToArray();
                }

                houses.Add(houseEntity);
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