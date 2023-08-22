// <copyright file="EndDateAfterStartDateAttribute.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DomainModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Models;

    /// <summary>
    ///   Custom validator for auction end date.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class EndDateAfterStartDateAttribute : ValidationAttribute
    {
        /// <summary>Checks whether the endDate is after the startDate or not.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="ValidationResult">ValidationResult</see> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime endDate = (DateTime)value;
            Product product = (Product)validationContext.ObjectInstance;

            if (endDate > product.StartDate)
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
