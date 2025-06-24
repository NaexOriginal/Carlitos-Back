using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Carlitos5G.Validation
{
    public class ConditionalImageValidationAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly int _maxFileSizeMB = 5;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var imageFile = value as IFormFile;
            var isEditProp = validationContext.ObjectType.GetProperty("IsEdit");

            if (isEditProp == null)
            {
                return new ValidationResult("No se pudo determinar si es edición o creación.");
            }

            bool isEdit = (bool)isEditProp.GetValue(validationContext.ObjectInstance);

            // En creación, la imagen es obligatoria
            if (!isEdit && imageFile == null)
            {
                return new ValidationResult("La imagen es obligatoria.");
            }

            // En edición, solo validar si se proporciona una nueva imagen
            if (imageFile != null)
            {
                var extension = Path.GetExtension(imageFile.FileName).ToLower();
                if (!_allowedExtensions.Contains(extension))
                {
                    return new ValidationResult("Tipo de archivo no permitido. Solo se aceptan imágenes JPG, JPEG, PNG o GIF.");
                }

                if (imageFile.Length > _maxFileSizeMB * 1024 * 1024)
                {
                    return new ValidationResult($"El tamaño del archivo no debe exceder {_maxFileSizeMB} MB.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
