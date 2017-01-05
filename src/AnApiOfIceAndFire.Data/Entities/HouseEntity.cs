namespace AnApiOfIceAndFire.Data.Entities
{
    public class HouseEntity : BaseEntity
    {
        public string CoatOfArms { get; set; } = string.Empty;
        public string Words { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Founded { get; set; } = string.Empty;
        public string DiedOut { get; set; } = string.Empty;
        public string[] Seats { get; set; } = new string[0];
        public string[] Titles { get; set; } = new string[0];
        public string[] AncestralWeapons { get; set; } = new string[0];

        public int? FounderId { get; set; }
        public int? CurrentLordId { get; set; }
        public int? HeirId { get; set; }
        public int? OverlordId { get; set; }

        public int[] SwornMembers { get; set; } = new int[0];
        public int[] CadetBranches { get; set; } = new int[0];
    }
}