using FilesUpload.Models;

namespace FilesUpload.Services
{
    public interface IBlobService
    {
        Task<string> UploadFileAsync(UploadModel model);
    }
}
