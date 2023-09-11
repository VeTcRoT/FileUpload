using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace FilesUpload.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;

        public MaxFileSizeAttribute(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IBrowserFile file)
            {
                if (file.Size > _maxFileSize)
                {
                    return new ValidationResult(
                        string.IsNullOrEmpty(ErrorMessage) 
                        ? $"Maximum allowed file size is {_maxFileSize / 1000000} megabytes." 
                        : ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
