using System;
using System.Threading.Tasks;
using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using NUnit.Framework;

namespace AcceptanceTests.Common.AudioRecordings
{
    public class AzureStorageManager
    {
        private string _storageAccountName;
        private string _storageAccountKey;
        private string _storageContainerName;
        private BlobClient _blobClient;

        public AzureStorageManager SetStorageAccountName(string storageAccountName)
        {
            _storageAccountName = storageAccountName;
            return this;
        }

        public AzureStorageManager SetStorageAccountKey(string storageAccountKey)
        {
            _storageAccountKey = storageAccountKey;
            return this;
        }

        public AzureStorageManager SetStorageContainerName(string storageContainerName)
        {
            _storageContainerName = storageContainerName;
            return this;
        }

        public AzureStorageManager CreateBlobClient(Guid hearingId)
        {
            var containerClient = CreateContainerClient();
            _blobClient = containerClient.GetBlobClient($"{hearingId}.mp4");
            return this;
        }

        private BlobContainerClient CreateContainerClient()
        {
            var storageSharedKeyCredential = new StorageSharedKeyCredential(_storageAccountName, _storageAccountKey);
            var serviceEndpoint = $"https://{_storageAccountName}.blob.core.windows.net/";
            var serviceClient = new BlobServiceClient(new Uri(serviceEndpoint), storageSharedKeyCredential);
            return serviceClient.GetBlobContainerClient(_storageContainerName);
        }

        public async Task UploadAudioFileToStorage(string file)
        {
            await _blobClient.UploadAsync(file);

            if (!await _blobClient.ExistsAsync())
            {
                throw new RequestFailedException($"Can not find file: {file}");
            }

            TestContext.WriteLine($"Uploaded audio file to : {file}");
        }

        public async Task<bool> VerifyAudioFileExistsInStorage()
        {
            return await _blobClient.ExistsAsync();
        }

        public async Task RemoveAudioFileFromStorage()
        {
            await _blobClient.DeleteAsync();
            TestContext.WriteLine($"Deleted audio file");
        }
    }
}
