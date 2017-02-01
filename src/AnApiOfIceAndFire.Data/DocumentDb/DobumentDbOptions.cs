namespace AnApiOfIceAndFire.Data.DocumentDb
{
    public class DobumentDbOptions
    {
        public string DatabaseName { get; set; }
        public string EndpointUri { get; set; }
        public string PrimaryKey { get; set; }
        public int Throughput { get; set; }

        public string BookCollectionName { get; set; }
        public string BookTypeName { get; set; }

        public string CharacterCollectionName { get; set; }
        public string CharacterTypeName { get; set; }

        public string HouseCollectionName { get; set; }
        public string HouseTypeName { get; set; }

        public string GetCollectionName<TEntity>()
        {
            var typeName = typeof(TEntity).Name;

            if(string.Equals(typeName, BookTypeName))
            {
                return BookCollectionName;
            }
            if(string.Equals(typeName, CharacterTypeName))
            {
                return CharacterCollectionName;
            }
            if(string.Equals(typeName, HouseTypeName))
            {
                return HouseCollectionName;
            }

            return string.Empty;
        }

        public string GetTypeName<TEntity>()
        {
            var typeName = typeof(TEntity).Name;

            if (string.Equals(typeName, BookTypeName))
            {
                return BookTypeName;
            }
            if (string.Equals(typeName, CharacterTypeName))
            {
                return CharacterTypeName;
            }
            if (string.Equals(typeName, HouseTypeName))
            {
                return HouseTypeName;
            }

            return string.Empty;
        }
    }
}