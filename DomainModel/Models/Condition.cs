// <copyright file="Condition.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DomainModel.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>The Condition model.</summary>
    public class Condition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Condition"/> class.
        /// </summary>
        /// <param name="name">The name of the condition.</param>
        /// <param name="description">The description of the condition.</param>
        /// <param name="value">The value of the condition.</param>
        public Condition(string name, string description, int value)
        {
            this.Name = name;
            this.Description = description;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Condition"/> class.
        /// </summary>
        public Condition()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; private set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "[Name] cannot be null.")]
        [StringLength(maximumLength: 15, MinimumLength = 1, ErrorMessage = "[Name] must be between 1 and 15 characters.")]
        public virtual string Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [Required(ErrorMessage = "[Description] cannot be null.")]
        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "[Description] must be between 1 and 100 characters.")]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        [Required(ErrorMessage = "[Value] cannot be null.")]
        public virtual int Value { get; set; }
    }
}
