using System;
using System.Collections.Generic;
using System.Linq;
using AnApiOfIceAndFire.Data.Entities;
using AnApiOfIceAndFire.Domain.Books;
using Xunit;

namespace AnApiOfIceAndFire.Domain.Tests.Books
{
    public class BookFilterTests
    {
        [Fact]
        public void NullQueryable_WhenApplying_ThenEmptyQueryableIsReturned()
        {
            var filter = new BookFilter();

            var filteredBooks = filter.Apply(null).ToList();

            Assert.Equal(0, filteredBooks.Count);
        }

        [Fact] 
        public void NoPropertiesSetOnFilter_WhenApplying_ThenNoBooksAreFiltered()
        {
            var filter = new BookFilter();
            var books = new List<BookEntity>
            {
                new BookEntity {Id = 1, Name = "A Game Of Thrones"},
                new BookEntity {Id = 2, Name = "A Clash Of Kings"}
            };

            var filteredBooks = filter.Apply(books.AsQueryable());

            Assert.Equal(books.Count, filteredBooks.Count());
        }

        [Fact]
        public void NameSet_WhenApplying_ThenBooksWithDifferentNameAreFiltered()
        {
            var filter = new BookFilter()
            {
                Name = "A Game Of Thrones"
            };
            var books = new List<BookEntity>
            {
                new BookEntity {Id = 1, Name = "A Game Of Thrones"},
                new BookEntity {Id = 2, Name = "A Clash Of Kings"}
            };

            var filteredBooks = filter.Apply(books.AsQueryable()).ToList();

            Assert.Equal(1, filteredBooks.Count);
            Assert.Equal(books[0].Name, filteredBooks[0].Name);
        }

        [Fact]
        public void FromReleaseDateSet_WhenApplying_ThenBooksWithAnEarlierReleaseDateAreFiltered()
        {
            var filter = new BookFilter()
            {
                FromReleaseDate = new DateTime(1997,1,1)
            };
            var books = new List<BookEntity>
            {
                new BookEntity {Id = 1, Name = "A Game Of Thrones", ReleaseDate = new DateTime(1996,8,1)},
                new BookEntity {Id = 2, Name = "A Clash Of Kings", ReleaseDate = new DateTime(1998,1,1)}
            };

            var filteredBooks = filter.Apply(books.AsQueryable()).ToList();
            
            Assert.Equal(1, filteredBooks.Count);
            Assert.Equal(books[1].Id, filteredBooks[0].Id);
        }

        [Fact]
        public void ToReleaseDateSet_WhenApplying_ThenBooksWithLaterReleaseDateAreFiltered()
        {
            var filter = new BookFilter()
            {
                ToReleaseDate = new DateTime(1997, 1, 1)
            };
            var books = new List<BookEntity>
            {
                new BookEntity {Id = 1, Name = "A Game Of Thrones", ReleaseDate = new DateTime(1996,8,1)},
                new BookEntity {Id = 2, Name = "A Clash Of Kings", ReleaseDate = new DateTime(1998,1,1)}
            };

            var filteredBooks = filter.Apply(books.AsQueryable()).ToList();

            Assert.Equal(1, filteredBooks.Count);
            Assert.Equal(books[0].Id, filteredBooks[0].Id);
        }
    }
}