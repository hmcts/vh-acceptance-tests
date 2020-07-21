namespace AcceptanceTests.TestAPI.Domain.Ddd
{
    public interface IEntity<out TKey>  
    {
        TKey Id { get; }
    }
}