using combofind.Resources;

namespace combofind.Domain.Entities
{
    public class GunEntity : BaseEntity
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Quality { get; private set; }
        public string Class { get; private set; }
        public string Condition { get; private set; }
        public string MainColor { get; private set; }
        public decimal AveragePrice { get; private set; }
        public string Image { get; private set; }
        public Guid CollectionID { get; private set; }
        public Collection Collection { get; private set; }

        private GunEntity() { }
        
        public GunEntity(string name, string type, string quality, string classType, string condition, string mainColor, decimal averagePrice, string image, Collection collection)
        {
            Validations(name, type, quality, classType, condition, mainColor, averagePrice, image);

            Name = name;
            Type = type;
            Quality = quality;
            Class = classType;
            Condition = condition;
            MainColor = mainColor;
            AveragePrice = averagePrice;
            Image = image;

            AssignCollection(collection);
        }
        public void AssignCollection(Collection collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            Collection = collection;
            CollectionID = collection.Id;
        }
        public void UpdateData(string name, string type, string quality, string classType, string condition, string mainColor, decimal averagePrice, string image)
        {
            Validations(name, type, quality, classType, condition, mainColor, averagePrice, image);

            Name = name;
            Type = type;
            Quality = quality;
            Class = classType;
            Condition = condition;
            MainColor = mainColor;
            AveragePrice = averagePrice;
            Image = image;

            UpdateDate();
        }
        public void Validations(string name, string type, string quality, string classType, string condition, string mainColor, decimal averagePrice, string image)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ResourceErrorMessages.NameRequired, nameof(name));

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException(ResourceErrorMessages.TypeRequired, nameof(type));

            if (string.IsNullOrWhiteSpace(quality))
                throw new ArgumentException(ResourceErrorMessages.QualityRequired, nameof(quality));

            if (string.IsNullOrWhiteSpace(classType))
                throw new ArgumentException(ResourceErrorMessages.ClassRequired, nameof(classType));

            if (string.IsNullOrWhiteSpace(condition))
                throw new ArgumentException(ResourceErrorMessages.ConditionRequired, nameof(condition));

            if (string.IsNullOrWhiteSpace(mainColor))
                throw new ArgumentException(ResourceErrorMessages.ColorRequired, nameof(mainColor));

            if(averagePrice < 0)
                throw new ArgumentException(ResourceErrorMessages.NegativePrice, nameof(averagePrice));

            if (string.IsNullOrWhiteSpace(image))
                throw new ArgumentException(ResourceErrorMessages.ImageRequired, nameof(image));
        }
    }
}
