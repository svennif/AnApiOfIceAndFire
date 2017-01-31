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

                //Calculate mappings, example: which characters should belong to a given book.

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