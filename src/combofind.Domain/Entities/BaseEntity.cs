namespace combofind.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTimeOffset DateCreated { get; private set; }
        public DateTimeOffset? DateUpdated { get; private set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTimeOffset.UtcNow;
        }
        public void UpdateDate()
        {
            DateUpdated = DateTimeOffset.UtcNow;
        }

    }
}
