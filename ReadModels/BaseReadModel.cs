namespace MBACNationals.ReadModels
{
    public abstract class BaseReadModel<TModel>
    {
        public class Entity : AzureReadModel.Entity { }
        public class Blob : AzureReadModel.Blob { }

        public BaseReadModel()
        {
            Storage = new AzureReadModel(typeof(TModel).Name);
        }

        public AzureReadModel Storage { get; private set; }
    }
}
