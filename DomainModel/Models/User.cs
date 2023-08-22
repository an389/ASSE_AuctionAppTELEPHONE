// <copyright file="User.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace DomainModel.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    using DomainModel.Enums;

    /// <summary>The User model.</summary>
    public class User
    {
        /// <summary>Initializes a new instance of the <see cref="User" /> class.</summary>
        /// <param name="firstName">User's first name.</param>
        /// <param name="lastName">User's last name.</param>
        /// <param name="userName">User's username.</param>
        /// <param name="phoneNumber">User's phone number.</param>
        /// <param name="email">User's email address.</param>
        /// <param name="password">User's password.</param>
        public User(string firstName, string lastName, string userName, string phoneNumber, string email, string password)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserName = userName;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.Password = password;
            this.AccountType = EAccountType.New;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; private set; }

        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        [Required(ErrorMessage = "[FirstName] cannot be null.")]
        [StringLength(maximumLength: 15, MinimumLength = 1, ErrorMessage = "[FirstName] must be between 1 and 15 characters.")]
        [RegularExpression(@"[A-Z][a-z]+", ErrorMessage = "[FirstName] must be a valid firstname.")]
        public virtual string FirstName { get; set; }

        /// <summary>Gets or sets the last name.</summary>
        /// <value>The last name.</value>
        [Required(ErrorMessage = "[LastName] cannot be null.")]
        [StringLength(maximumLength: 15, MinimumLength = 1, ErrorMessage = "[LastName] must be between 1 and 15 characters.")]
        [RegularExpression(@"[A-Z][a-z]+", ErrorMessage = "[LastName] must be a valid lastname.")]
        public virtual string LastName { get; set; }

        /// <summary>Gets the username.</summary>
        /// <value>The name of the user.</value>
        [Required(ErrorMessage = "[UserName] cannot be null.")]
        [StringLength(maximumLength: 30, MinimumLength = 1, ErrorMessage = "[UserName] must be between 1 and 30 characters.")]
        public virtual string UserName { get; private set; }

#nullable enable
        /// <summary>Gets or sets the phone number.</summary>
        /// <value>The phone number.</value>
        [Phone(ErrorMessage = "[PhoneNumber] is not a valid phone number.")]
        [StringLength(maximumLength: 15, MinimumLength = 1, ErrorMessage = "[PhoneNumber] must have between 1 and 15 digits.")]
        public virtual string? PhoneNumber { get; set; }
#nullable disable

        /// <summary>Gets the email.</summary>
        /// <value>The email.</value>
        [Required(ErrorMessage = "[Email] cannot be null.")]
        [EmailAddress(ErrorMessage = "[Email] is not a valid email address.")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "[Email] must have between 5 and 50 digits.")]
        public virtual string Email { get; private set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        [Required(ErrorMessage = "[Password] cannot be null.")]
        [StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = "[Password] must be between 8 and 20 characters.")]
        [CustomValidation(typeof(User), "IsValidPassword", ErrorMessage = "[Password] must contain at least one number, one uppercase letter, one lowercase letter and one symbol.")]
        public virtual string Password { get; set; }

        /// <summary>Gets or sets the type of the account.</summary>
        /// <value>The type of the account.</value>
        [Required(ErrorMessage = "[AccountType] cannot be null.")]
        public virtual EAccountType AccountType { get; set; }

        /// <summary>Determines whether [is valid password] [the specified password].</summary>
        /// <param name="password">The password.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        ///    <see cref="ValidationResult.Success"/> if the password is a valid password.
        ///    <br/>
        ///    <see cref="ValidationResult(string?)"/> if the password is not a valid password.
        /// </returns>
        public static ValidationResult IsValidPassword(string password, ValidationContext context)
        {
            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasUpperCaseLetter = new Regex(@"[A-Z]+");
            Regex hasLowerCaseLetter = new Regex(@"[a-z]+");
            Regex hasSpecialCharacter = new Regex(@"[!?@#$%&_]+");

            bool isValid =
                hasNumber.IsMatch(password) &&
                hasUpperCaseLetter.IsMatch(password) &&
                hasLowerCaseLetter.IsMatch(password) &&
                hasSpecialCharacter.IsMatch(password);

            return (isValid == true) ? ValidationResult.Success : new ValidationResult(null);
        }
    }
}
