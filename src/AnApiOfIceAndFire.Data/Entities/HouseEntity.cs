namespace AnApiOfIceAndFire.Data.Entities
{
    public class HouseEntity : BaseEntity
    {
        public string CoatOfArms { get; set; }
        public string Words { get; set; }
        public string Region { get; set; }
        public string Founded { get; set; }
        public string DiedOut { get; set; }
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