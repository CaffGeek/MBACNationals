using AzureTableHelper;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using System.Configuration;
namespace MBACNationals.ReadModels
{
    public class ReadModelPersister
    {
        public static void Save(dynamic readModel)
        {
            var jsonModel = JsonConvert.SerializeObject(readModel);

            var storageConnection = ConfigurationManager.ConnectionStrings["AzureTableStorage"].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(storageConnection);
            var blobClient = storageAccount.CreateCloudBlobClient();

            var azureBlobHelper = new AzureBlobHelper(blobClient);
            var container = azureBlobHelper.GetContainerFor("ReadModels");

            var modelBlob = container.GetBlockBlobReference(readModel.GetType().Name);
            modelBlob.UploadText(jsonModel);
        }
    }
}
