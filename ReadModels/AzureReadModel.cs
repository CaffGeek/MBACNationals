using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MBACNationals.ReadModels
{
    public abstract class AzureReadModel
    {
        public class Entity : TableEntity
        {
            public string AzureEntityType { get; set; }
        }

        private CloudTable Table { get; set; }

        public AzureReadModel()
        {
            var tableName = this.GetType().Name;
            var tableStorageConn = ConfigurationManager.ConnectionStrings["AzureTableStorage"].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(tableStorageConn);
            var tableClient = storageAccount.CreateCloudTableClient();
            Table = tableClient.GetTableReference(tableName);
            Table.CreateIfNotExists();
        }

        protected void Create<T>(Guid partition, Guid key, T entity)
            where T : Entity, new()
        {
            if (partition == null || key == null)
                return;

            entity.PartitionKey = partition.ToString();
            entity.RowKey = key.ToString();
            entity.AzureEntityType = typeof(T).Name;

            
            Table.Execute(TableOperation.InsertOrReplace(entity));
        }

        protected T Read<T>(Guid key)
            where T : Entity, new()
        {
            var entity = Query<T>(x => { return x.RowKey == key.ToString(); }).FirstOrDefault();
            return entity;
        }

        protected T Read<T>(Guid partition, Guid key)
            where T : Entity, new()
        {
            
            var tableResults = Table.Execute(TableOperation.Retrieve<T>(partition.ToString(), key.ToString()));
            var entity = (T)tableResults.Result;
            return entity;
        }

        protected List<T> Query<T>()
            where T : Entity, new()
        {
            
            return Table.CreateQuery<T>().Where(x => x.AzureEntityType.Equals(typeof(T).Name)).ToList();
        }

        protected List<T> Query<T>(Func<T, bool> predicate)
            where T : Entity, new()
        {
            
            return Table.CreateQuery<T>().Where(predicate).Where(x => x.AzureEntityType.Equals(typeof(T).Name)).ToList();
        }

        protected void Update<T>(Guid partition, Guid key, Action<T> func)
            where T : Entity, new()
        {
            var entity = Read<T>(partition, key);
            func(entity);

            Table.Execute(TableOperation.Replace(entity));
        }

        protected void Update<T>(Guid key, Action<T> func)
            where T : Entity, new()
        {
            var entity = Read<T>(key);
            func(entity);

            Table.Execute(TableOperation.Replace(entity));
        }

        protected void Delete<T>(Guid partition, Guid key)
            where T : Entity, new()
        {
            var entity = Read<T>(partition, key);
            
            Table.Execute(TableOperation.Delete(entity));
        }
    }
}
