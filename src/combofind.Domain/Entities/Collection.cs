namespace combofind.Domain.Entities
{
    public class Collection : BaseEntity
    {
        public string Color { get; private set; }
        public string Budget { get; private set; }
        public List<Guns> Guns { get; private set; }

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

        public void AddGun(Guns gun)
        {
            Guns.Add(gun);
        }
        public void UpdateColor(string newColor)
        {
            if (string.IsNullOrWhiteSpace(newColor))
                throw new ArgumentException("Color cannot be empty.", nameof(newColor));

            Color = newColor;
            UpdateDate();
        }
        public void UpdateBudget(string newBudget)
        {
            if (string.IsNullOrWhiteSpace(newBudget))
                throw new ArgumentException("Budget cannot be empty.", nameof(newBudget));

            Budget = newBudget;
            UpdateDate();
        }
    }
}
