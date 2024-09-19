namespace combofind.Application.UseCases.GunsUseCases.Common
{
    public class GunResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Quality { get; set; }
        public string Class { get; set; }
        public string Condition { get; set; }
        public string MainColor { get; set; }
        public decimal AveragePrice { get; set; }
        public string Image { get; set; }
    }
}
