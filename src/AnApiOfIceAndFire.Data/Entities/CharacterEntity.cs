namespace AnApiOfIceAndFire.Data.Entities
{
    public class CharacterEntity : BaseEntity
    {
        public string Culture { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }
        public bool? IsFemale { get; set; }
        public string[] Aliases { get; set; } = new string[0];
        public string[] TvSeries { get; set; } = new string[0];
        public string[] PlayedBy { get; set; } = new string[0];

        public int? FatherId { get; set; }
        public int? MotherId { get; set; }
        public int? SpouseId { get; set; }

        public int[] Allegiances { get; set; } = new int[0];
        public int[] Books { get; set; } = new int[0];
        public int[] PovBooks { get; set; } = new int[0];
    }
}