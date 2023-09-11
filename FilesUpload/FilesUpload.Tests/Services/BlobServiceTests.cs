using Azure.Storage.Blobs;
using FilesUpload.Models;
using FilesUpload.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FilesUpload.Tests.Services
{
    public class BlobServiceTests
    {
        private readonly Mock<BlobServiceClient> _blobServiceClientMock;
        private readonly Mock<BlobContainerClient> _blobContainerClientMock;
        private readonly Mock<BlobClient> _blobClientMock;
        private readonly UploadModel _uploadModel;
        private readonly Mock<IBrowserFile> _browserFileMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly string _blobName;
        private readonly IBlobService _blobService;

        public BlobServiceTests()
        {
            _blobServiceClientMock = new();
            _blobContainerClientMock = new();
            _blobClientMock = new();
            _uploadModel = new();
            _browserFileMock = new();
            _configurationMock = new();
            _blobName = "testBlobName.txt";
            _blobService = new BlobService(_blobServiceClientMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task UploadFileAsync_Invoke_ReturnsBlobName()
        {
            //Arrange
            _blobServiceClientMock.Setup(b => b.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(_blobContainerClientMock.Object);

            _blobContainerClientMock.Setup(b => b.GetBlobClient(It.IsAny<string>()))
                .Returns(_blobClientMock.Object);

            _uploadModel.Email = "testemail@localhost";
            _uploadModel.File = _browserFileMock.Object;

            _browserFileMock.Setup(f => f.Name)
                .Returns(_blobName);

            _configurationMock.Setup(x => x["Azure:AzureBlobContainerName"])
                .Returns("testContainerName");

            //Act
            var result = await _blobService.UploadFileAsync(_uploadModel);

            //Assert
            Assert.Contains(_blobName, result);
        }
    }
}
