using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace FilesUpload.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(params string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IBrowserFile file)
            {
                var extension = Path.GetExtension(file.Name);
                if (string.IsNullOrEmpty(extension) || Array.IndexOf(_extensions, extension.ToLower()) == -1)
                {
                    return new ValidationResult(
                        string.IsNullOrEmpty(ErrorMessage) 
                        ? $"Only files with extensions {string.Join(", ", _extensions)} are allowed." 
                        : ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
