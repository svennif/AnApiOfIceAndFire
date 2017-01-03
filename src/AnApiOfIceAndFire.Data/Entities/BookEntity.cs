using System;

namespace AnApiOfIceAndFire.Data.Entities
{
    public class BookEntity : BaseEntity
    {
        public string ISBN { get; set; }
        public string[] Authors { get; set; } = new string[0];
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public int MediaTypeId { get; set; }
        public string Country { get; set; }
        public DateTime ReleaseDate { get; set; } //DocumentDB does not have support for DateTime, we need to store epoch time or something similar

        public int[] Characters { get; set; } = new int[0];
        public int[] PovCharacters { get; set; } = new int[0];
    }
}