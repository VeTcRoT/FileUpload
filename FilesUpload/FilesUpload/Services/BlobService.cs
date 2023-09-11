using Azure.Storage.Blobs;
using FilesUpload.Models;

namespace FilesUpload.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;

        private const int _maxAllowedFileSize = 5000000;

        public BlobService(
            BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }

        public async Task<string> UploadFileAsync(UploadModel model)
        {
            var containerClient = _blobServiceClient
                    .GetBlobContainerClient(_configuration["Azure:AzureBlobContainerName"]);

            await containerClient.CreateIfNotExistsAsync();

            var blobName = $"{Guid.NewGuid() + "_" + model.File.Name}";
            var blobClient = containerClient.GetBlobClient(blobName);

            using (var stream = model.File.OpenReadStream(_maxAllowedFileSize))
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobName;
        }
    }
}
