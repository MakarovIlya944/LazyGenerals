namespace LazyGenerals.Server.Models.Entities
{
    interface IEntity<T>
    {
        T Id { get; }
    }
}
