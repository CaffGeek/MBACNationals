using AzureTableHelper;
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
        {
            var container = GetContainer();
            CloudBlockBlob modelBlob = container.GetBlockBlobReference(typeof(T).Name);
            var text = modelBlob.DownloadText();
            return JsonConvert.DeserializeObject<T>(text);
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
