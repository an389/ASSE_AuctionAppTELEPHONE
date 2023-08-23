using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Utilizator
    {

        [ExcludeFromCodeCoverage]
        public virtual int Id { get; private set; }

        [Required(ErrorMessage = "[FirstName] cannot be null.")]
        [StringLength(maximumLength: 15, MinimumLength = 1, ErrorMessage = "[FirstName] must be between 1 and 15 characters.")]
        [RegularExpression(@"[A-Z][a-z]+", ErrorMessage = "[FirstName] must be a valid firstname.")]
        public virtual string FirstName { get; set; }

        [Required(ErrorMessage = "[LastName] cannot be null.")]
        [StringLength(maximumLength: 15, MinimumLength = 1, ErrorMessage = "[LastName] must be between 1 and 15 characters.")]
        [RegularExpression(@"[A-Z][a-z]+", ErrorMessage = "[LastName] must be a valid lastname.")]
        public virtual string LastName { get; set; }
       
        [Required(ErrorMessage = "[Email] cannot be null.")]
        [EmailAddress(ErrorMessage = "[Email] is not a valid email address.")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "[Email] must have between 5 and 50 digits.")]
        public virtual string Emali { get; set; }
      
        [Required(ErrorMessage = "[Password] cannot be null.")]
        [StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = "[Password] must be between 8 and 20 characters.")]
        [CustomValidation(typeof(Utilizator), "IsValidPassword", ErrorMessage = "[Password] must contain at least one number, one uppercase letter, one lowercase letter and one symbol.")]
        public virtual string Password { get; set; }

        [Required(ErrorMessage = "[CNP] cannot be null.")]
        [CustomValidation(typeof(Utilizator), "IsValidCNP", ErrorMessage = "[CNP] must be 13 nubers. Start with 1,2,5,6(sex) and the next 6 nr represent date of birth  OR USER UNDER AGE 18")]
        public virtual long CNP { get; set; }
        [Required(ErrorMessage = "[BonusuriId] cannot be null.")]
        public string BonusuriId { get; set; }
        [Required(ErrorMessage = "[EsteBunDePlata] cannot be null.")]
        public bool EsteBunDePlata { get; set; }


        public Utilizator(string firstName, string lastName, string emali, string password, long CNP, string bonusuriId)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Emali = emali;
            this.Password = password;
            this.CNP = CNP;
            this.EsteBunDePlata = true;
            this.BonusuriId = bonusuriId;
      
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Utilizator"/> class.
        /// </summary>
        ///
        [ExcludeFromCodeCoverage]
        public Utilizator()
        {
        }

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

        public static ValidationResult IsValidCNP(long cNP, ValidationContext context)
        {

            long[] cnp = new long[13];
            if (cNP < 0)
            {
                return new ValidationResult("[CNP] cannot be negative");
            }

            int i = 12;
            while (cNP > 0)
            {

                cnp[i] = cNP % 10;
                cNP /= 10;
                i--;
            }

            if (cnp[0] is not 1 and not 2 and not 5 and not 6)
            {
                return new ValidationResult("Incorect first number from CNP");
            }

            if (cnp[3] > 1)
            {
                return new ValidationResult("[CNP] Incorect month!");
            }


            if (cnp[5] > 3)
            {
                return new ValidationResult("[CNP] Incorect day!!");
            }

            if (cnp[5] > 2 && cnp[6] > 2)
            {
                return new ValidationResult("[CNP] Incorect day!!(day cannot be grather than 32");
            }

            if (cnp[0] > 2 && ((cnp[1] * 10) + cnp[2]) > 18)
            {
                return new ValidationResult("Under AGE!!!");
            }

            return ValidationResult.Success;
        }

    }
}
