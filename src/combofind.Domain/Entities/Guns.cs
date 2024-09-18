namespace combofind.Domain.Entities
{
    public class Guns : BaseEntity
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Quality { get; private set; }
        public string Class { get; private set; }
        public string Condition { get; private set; }
        public string MainColor { get; private set; }
        public decimal AveragePrice { get; private set; }
        public string Image { get; private set; }

        private Guns() { }
        
        public Guns(string name, string type, string quality, string classType, string condition, string mainColor, decimal averagePrice, string image)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Type is required.", nameof(type));

            if (string.IsNullOrWhiteSpace(quality))
                throw new ArgumentException("Quality is required.", nameof(quality));

            if (string.IsNullOrWhiteSpace(classType))
                throw new ArgumentException("Class is required.", nameof(classType));

            if (string.IsNullOrWhiteSpace(condition))
                throw new ArgumentException("Condition is required.", nameof(condition));

            if (string.IsNullOrWhiteSpace(mainColor))
                throw new ArgumentException("Color is required.", nameof(mainColor));

            if (string.IsNullOrWhiteSpace(image))
                throw new ArgumentException("Image is required.", nameof(image));

            Name = name;
            Type = type;
            Quality = quality;
            Class = classType;
            Condition = condition;
            MainColor = mainColor;
            AveragePrice = averagePrice;
            Image = image;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("The price must be greater than zero.", nameof(newPrice));

            AveragePrice = newPrice;
            UpdateDate();
        }

    }
}
