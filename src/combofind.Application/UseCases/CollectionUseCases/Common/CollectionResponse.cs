namespace combofind.Application.UseCases.CollectionUseCases.Common
{
    public sealed record CollectionResponse
    {
        public Guid Id { get; set; }
        public string? Color { get; set; }
        public string? Budget { get; set; }
    }
}
