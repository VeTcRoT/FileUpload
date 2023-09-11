using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace FilesUpload.Models
{
    public class UploadModel
    {
        [Required(ErrorMessage = "Please select a .docx file.")]
        [DataType(DataType.Upload)]
        public IBrowserFile File { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a valid email.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;
    }
}
