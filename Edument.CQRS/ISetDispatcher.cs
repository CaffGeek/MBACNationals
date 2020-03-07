namespace Edument.CQRS
{
    public interface ISetDispatcher
    {
        void SetDispatcher(MessageDispatcher dispatcher);
    }
}
