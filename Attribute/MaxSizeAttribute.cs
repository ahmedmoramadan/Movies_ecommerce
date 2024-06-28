using System.ComponentModel.DataAnnotations;

namespace movies_ecommerce.Attribute
{
    public class MaxSizeAttribute : ValidationAttribute
    {
        private readonly int _MaxSize;
        public MaxSizeAttribute(int MaxSize)
        {
            _MaxSize = MaxSize;
        }
        protected override ValidationResult?
            IsValid(object? value, ValidationContext validationContext)
        {

            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _MaxSize)
                {
                    return new ValidationResult($"maximum allowed size is {_MaxSize} bytes");
                }
            }

            return ValidationResult.Success;
        }
    }
}
