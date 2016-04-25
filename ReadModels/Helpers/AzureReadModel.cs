using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.IO;

namespace MBACNationals.ReadModels
{
    public class AzureReadModel
    {
        public class Entity : TableEntity
        {
            public string AzureEntityType { get; set; }
        }

        public class Blob
        {
            public Guid Id { get; set; }
            public byte[] Contents { get; set; }
        }

        private AzureTableHelper.AzureTableHelper AzureTableHelper { get; set; }
        private AzureTableHelper.AzureBlobHelper AzureBlobHelper { get; set; }
        private string ModelName { get; set; }

        private CloudTable Table
        {
            get
            {
                return AzureTableHelper.GetTableFor(ModelName);
            }
        }

        private CloudBlobContainer Container
        {
            get
            {
                return AzureBlobHelper.GetContainerFor(ModelName);
            }
        }
        
        public AzureReadModel(string modelName)
        {
            ModelName = modelName;

            var tableStorageConn = ConfigurationManager.ConnectionStrings["AzureTableStorage"].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(tableStorageConn);
    
            var servicePoint = ServicePointManager.FindServicePoint(storageAccount.TableEndpoint);
            servicePoint.UseNagleAlgorithm = false;
            servicePoint.Expect100Continue = false;
            servicePoint.ConnectionLimit = 100;

            var tableClient = storageAccount.CreateCloudTableClient();
            AzureTableHelper = new AzureTableHelper.AzureTableHelper(tableClient);

            var blobClient = storageAccount.CreateCloudBlobClient();
            AzureBlobHelper = new AzureTableHelper.AzureBlobHelper(blobClient);
        }

        public void Create<T>(Guid partition, Guid key, T entity)
            where T : Entity, new()
        {
            entity.PartitionKey = partition.ToString();
            entity.RowKey = key.ToString();
            entity.AzureEntityType = typeof(T).Name;
            
            Table.Execute(TableOperation.InsertOrReplace(entity));
        }

        public void Create<T>(T blob)
            where T : Blob, new()
        {
            var azureBlobType = typeof(T).Name;
            var key = azureBlobType + blob.Id;
            var blockBlob = Container.GetBlockBlobReference(key);
            
            using (var stream = new MemoryStream(blob.Contents, writable: false))
            {
                blockBlob.UploadFromStream(stream);
            }
        }

        public T Read<T>(Guid key)
            where T : Entity, new()
        {
            var entity = Query<T>(x => x.RowKey == key.ToString()).FirstOrDefault();
            return entity;
        }

        public T ReadBlob<T>(Guid id)
            where T : Blob, new()
        {
            var azureBlobType = typeof(T).Name;
            var key = azureBlobType + id;
            var blockBlob = Container.GetBlockBlobReference(key);

            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
                return new T()
                {
                    Id = id,
                    Contents = memoryStream.ToArray()
                };
            }
        }

        public T Read<T>(Guid partition, Guid key)
            where T : Entity, new()
        {            
            var tableResults = Table.Execute(TableOperation.Retrieve<T>(partition.ToString(), key.ToString()));
            var entity = (T)tableResults.Result;
            return entity;
        }

        public List<T> Query<T>()
            where T : Entity, new()
        {            
            return Table.CreateQuery<T>().Where(x => x.AzureEntityType.Equals(typeof(T).Name)).ToList();
        }

        public List<T> Query<T>(Func<T, bool> predicate)
            where T : Entity, new()
        {            
            return Table.CreateQuery<T>().Where(x => x.AzureEntityType.Equals(typeof(T).Name)).Where(predicate).ToList();
        }

        public void Update<T>(Guid partition, Guid key, Action<T> func)
            where T : Entity, new()
        {
            var entity = Read<T>(partition, key);
            func(entity);

            Table.Execute(TableOperation.Replace(entity));
        }

        public void Update<T>(Guid key, Action<T> func)
            where T : Entity, new()
        {
            var entity = Read<T>(key);
            func(entity);

            Table.Execute(TableOperation.Replace(entity));
        }

        public void Delete<T>(Guid partition, Guid key)
            where T : Entity, new()
        {
            var entity = Read<T>(partition, key);
            
            Table.Execute(TableOperation.Delete(entity));
        }
    }
}
