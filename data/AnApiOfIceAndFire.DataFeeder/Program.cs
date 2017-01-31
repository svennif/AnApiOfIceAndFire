using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

                //Calculate mappings, example: which characters should belong to a given book.

                //Upsert data

                //Add option to either load data or dump data depending on arguments given

            }, "Total time");
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