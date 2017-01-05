using System;
using System.Linq;
using AnApiOfIceAndFire.Data.Entities;

namespace AnApiOfIceAndFire.Domain.Books
{
    public class BookFilter : IEntityFilter<BookEntity>
    {
        public string Name { get; set; }
        public DateTime? FromReleaseDate { get; set; }
        public DateTime? ToReleaseDate { get; set; }

        public IQueryable<BookEntity> Apply(IQueryable<BookEntity> querySet)
        {
            if (querySet == null) return Enumerable.Empty<BookEntity>().AsQueryable();

            //We only check null on the name since you might want to retrieve all characters with no name, which is represented as an empty string
            if (Name != null) 
            {
                querySet = querySet.Where(b => b.Name.Equals(Name));
            }
            if (FromReleaseDate.HasValue)
            {
                querySet = querySet.Where(b => b.ReleaseDate >= FromReleaseDate.Value);
            }
            if (ToReleaseDate.HasValue)
            {
                querySet = querySet.Where(b => b.ReleaseDate <= ToReleaseDate.Value);
            }

            return querySet;
        }
    }
}