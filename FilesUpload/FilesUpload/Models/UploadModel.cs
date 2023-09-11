﻿using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace FilesUpload.Models
{
    public class UploadModel
    {
        [Required(ErrorMessage = "File is required.")]
        [DataType(DataType.Upload)]
        public IBrowserFile File { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;
    }
}