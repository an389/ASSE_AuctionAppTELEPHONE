// <copyright file="Rating.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DomainModel.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>The Rating model.</summary>
    public class Rating
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rating"/> class.
        /// </summary>
        /// <param name="product">The product that was sold.</param>
        /// <param name="ratingUser">The user that is giving the rating.</param>
        /// <param name="ratedUser">The user that is given the rating.</param>
        /// <param name="grade">The grade given to the RatedUser by the RatingUser.</param>
        public Rating(Product product, User ratingUser, User ratedUser, int grade)
        {
            this.DateAndTime = DateTime.Now;
            this.Product = product;
            this.RatingUser = ratingUser;
            this.RatedUser = ratedUser;
            this.Grade = grade;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rating"/> class.
        /// </summary>
        public Rating()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; private set; }

        /// <summary>Gets the date and time.</summary>
        /// <value>The date and time.</value>
        [Required(ErrorMessage = "[DateAndTime] cannot be null.")]
        public virtual DateTime DateAndTime { get; private set; }

        /// <summary>Gets the product.</summary>
        /// <value>The product.</value>
        [Required(ErrorMessage = "[Product] cannot be null.")]
        [ObjectValidation(ErrorMessage = "[Product] must be a valid product.")]
        public virtual Product Product { get; private set; }

        /// <summary>Gets the rating user.</summary>
        /// <value>The rating user.</value>
        [Required(ErrorMessage = "[RatingUser] cannot be null.")]
        [ObjectValidation(ErrorMessage = "[RatingUser] must be a valid user.")]
        public virtual User RatingUser { get; private set; }

        /// <summary>Gets the rated user.</summary>
        /// <value>The rated user.</value>
        [Required(ErrorMessage = "[RatedUser] cannot be null.")]
        [ObjectValidation(ErrorMessage = "[RatedUser] must be a valid user.")]
        public virtual User RatedUser { get; private set; }

        /// <summary>Gets or sets the grade.</summary>
        /// <value>The grade.</value>
        [Required(ErrorMessage = "[Grade] cannot be null.")]
        [Range(1, 10, ErrorMessage = "[Grade] must be between 1 and 10.")]
        public virtual int Grade { get; set; }
    }
}
