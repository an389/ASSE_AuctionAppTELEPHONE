// <copyright file="ObjectValidationAttribute.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///   Custom validator for objects.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ObjectValidationAttribute : ValidationAttribute
    {
        /// <summary>Checks whether the object is valid or not.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="ValidationResult">ValidationResult</see> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = new ValidationContext(value, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(value, context, results, true))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(null);
            }
        }
    }
}
