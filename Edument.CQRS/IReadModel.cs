namespace Edument.CQRS
{
    public interface IReadModel
    {
        void Reset();
        void Save();
    }
}
