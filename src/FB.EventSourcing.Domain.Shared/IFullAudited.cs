namespace FB.EventSourcing.Domain.Shared
{
    public interface IFullAudited : IIdentifiable, IAudited, IDeletionAudited
    {
    }
}