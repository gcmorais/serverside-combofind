namespace combofind.Domain.Entities
{
    public class Collection : BaseEntity
    {
        public string Color { get; private set; }
        public string Budget { get; private set; }
        public ICollection<Guns> Guns { get; private set; } = new HashSet<Guns>();

        private Collection() { }
        
        public Collection(string color, string budget)
        {
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Color is required.", nameof(color));

            if (string.IsNullOrWhiteSpace(budget))
                throw new ArgumentException("Budget is required.", nameof(budget));

            Color = color;
            Budget = budget;
        }
    }
}
