using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Validations
{
    public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("O campo não pode ser nulo ou vazio.");
            }

            var primeiraLetra = value.ToString()[0].ToString();

            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                return new ValidationResult("A primeira letra deve ser maiúscula.");
            }

            return ValidationResult.Success;

        }
            
    }
}
