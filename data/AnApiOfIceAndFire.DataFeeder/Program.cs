using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

                var characterRelations = Time(() => MapCharacterRelations(characters), "Mapping of character relations");
                var mappedBooks = Time(() => MapBooks(books, characterRelations), "Mapping of book data");
                var mappedCharacters = Time(() => MapCharacters(characters), "Mapping of character data");
                var mappedHouses = Time(() => MapHouses(houses, characterRelations), "Mapping of house data");

                //Upsert data

                //Add option to either load data or dump data depending on arguments given

            }, "Total time");

            Console.ReadLine();
        }

        private static List<BookEntity> MapBooks(List<BookData> bookData, CharacterRelationsMapping characterRelations)
        {
            var books = new List<BookEntity>();

            foreach (var book in bookData)
            {
                var bookEntity = Mapper.Map<BookEntity>(book);
                bookEntity.Characters = characterRelations.GetCharacters(bookEntity.Id).ToArray();
                bookEntity.PovCharacters = characterRelations.GetPovCharacters(bookEntity.Id).ToArray();

                books.Add(bookEntity);
            }

            return books;
        }

        private static List<CharacterEntity> MapCharacters(List<CharacterData> characterData)
        {
            return characterData.Select(Mapper.Map<CharacterEntity>).ToList();
        }

        private static List<HouseEntity> MapHouses(List<HouseData> houseData, CharacterRelationsMapping characterRelations)
        {
            var houses = new List<HouseEntity>();

            foreach (var house in houseData)
            {
                var houseEntity = Mapper.Map<HouseEntity>(house);
                houseEntity.SwornMembers = characterRelations.GetSwornMembers(houseEntity.Id).ToArray();
                houses.Add(houseEntity);
            }

            return houses;
        }

        private static CharacterRelationsMapping MapCharacterRelations(List<CharacterData> characterData)
        {
            var bookCharacterMappings = new Dictionary<int, List<int>>();
            var povBookCharacterMappings = new Dictionary<int, List<int>>();
            var swornMembersMapping = new Dictionary<int, List<int>>();

            foreach (var character in characterData)
            {
                foreach (var book in character.Books)
                {
                    if (bookCharacterMappings.ContainsKey(book))
                    {
                        var list = bookCharacterMappings[book];
                        list.Add(character.Id);
                        bookCharacterMappings[book] = list;
                    }
                    else
                    {
                        var list = new List<int> { character.Id };
                        bookCharacterMappings[book] = list;
                    }
                }
                foreach (var povBook in character.PovBooks)
                {
                    if (povBookCharacterMappings.ContainsKey(povBook))
                    {
                        var list = povBookCharacterMappings[povBook];
                        list.Add(character.Id);
                        povBookCharacterMappings[povBook] = list;
                    }
                    else
                    {
                        var list = new List<int> { character.Id };
                        povBookCharacterMappings[povBook] = list;
                    }
                }
                foreach (var allegiance in character.Allegiances)
                {
                    if (swornMembersMapping.ContainsKey(allegiance))
                    {
                        var list = swornMembersMapping[allegiance];
                        list.Add(character.Id);
                        swornMembersMapping[allegiance] = list;
                    }
                    else
                    {
                        var list = new List<int> { character.Id };
                        swornMembersMapping[allegiance] = list;
                    }
                }
            }

            return new CharacterRelationsMapping(bookCharacterMappings, povBookCharacterMappings, swornMembersMapping);
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