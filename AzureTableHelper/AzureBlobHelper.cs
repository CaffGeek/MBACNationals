using Microsoft.WindowsAzure.Storage.Blob;
using System;

namespace AzureTableHelper
{
    public class AzureBlobHelper
    {
        private readonly CloudBlobClient _blobClient;

        public AzureBlobHelper(CloudBlobClient blobClient)
        {
            _blobClient = blobClient;
        }

        public CloudBlobContainer GetContainerFor(Type type)
        {
            return GetContainerFor(type.Name);
        }

        public CloudBlobContainer GetContainerFor(string typeName)
        {
            typeName = typeName.ToLowerInvariant();
            var container = _blobClient.GetContainerReference(typeName);
            container.CreateIfNotExists();
            return container;
        }
    }
}