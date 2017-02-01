using Newtonsoft.Json;

namespace AnApiOfIceAndFire.Data.Entities
{
    public class BaseEntity
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}