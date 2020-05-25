namespace FB.EventSourcing.Domain.Shared
{
    public interface IAudited : ICreationAudited, IModificationAudited
    {
    }
}