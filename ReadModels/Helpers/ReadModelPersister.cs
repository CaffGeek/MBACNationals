using AzureTableHelper;
using Edument.CQRS;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System.Configuration;

namespace MBACNationals.ReadModels
{
    public class ReadModelPersister
    {
        public static void Save(dynamic readModel)
        {
            var jsonModel = JsonConvert.SerializeObject(readModel);
            var container = GetContainer();
            CloudBlockBlob modelBlob = container.GetBlockBlobReference(readModel.GetType().Name);
            modelBlob.UploadText(jsonModel);
        }

        public static T Load<T>()
            where T : new()
        {
            var container = GetContainer();
            CloudBlockBlob modelBlob = container.GetBlockBlobReference(typeof(T).Name);
            if (modelBlob.Exists())
            {
                var text = modelBlob.DownloadText();
                return JsonConvert.DeserializeObject<T>(text);
            }
            else
            {
                return new T();
            }
        }

        private static CloudBlobContainer GetContainer()
        {
            var storageConnection = ConfigurationManager.ConnectionStrings["AzureTableStorage"].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(storageConnection);
            var blobClient = storageAccount.CreateCloudBlobClient();

            var azureBlobHelper = new AzureBlobHelper(blobClient);
            return azureBlobHelper.GetContainerFor("ReadModels");
        }
    }
}
