// <copyright file="UserValidation.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ModelValidationTests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Models;
    using NUnit.Framework;

    /// <summary>
    ///     Test class for <see cref="User"/> validation.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class UserValidation
    {
        /// <summary>Null first name log message.</summary>
        private const string LogNullFirstName = "[FirstName] cannot be null.";

        /// <summary>Too long first name log message.</summary>
        private const string LogTooLongFirstName = "[FirstName] must be between 1 and 15 characters.";

        /// <summary>Invalid first name log message.</summary>
        private const string LogInvalidFirstName = "[FirstName] must be a valid firstname.";

        /// <summary>Null last name log message.</summary>
        private const string LogNullLastName = "[LastName] cannot be null.";

        /// <summary>Invalid last name log message.</summary>
        private const string LogInvalidLastName = "[LastName] must be a valid lastname.";

        /// <summary>Too long last name log message.</summary>
        private const string LogTooLongLastName = "[LastName] must be between 1 and 15 characters.";

        /// <summary>Null username log message.</summary>
        private const string LogNullUserName = "[UserName] cannot be null.";

        /// <summary>Too long username log message.</summary>
        private const string LogTooLongUserName = "[UserName] must be between 1 and 30 characters.";

        /// <summary>Too long phone number log message.</summary>
        private const string LogTooLongPhoneNumber = "[PhoneNumber] must have between 1 and 15 digits.";

        /// <summary>Invalid phone number log message.</summary>
        private const string LogInvalidPhoneNumber = "[PhoneNumber] is not a valid phone number.";

        /// <summary>Null email log message.</summary>
        private const string LogNullEmail = "[Email] cannot be null.";

        /// <summary>Too long email log message.</summary>
        private const string LogTooLongEmail = "[Email] must have between 5 and 50 digits.";

        /// <summary>Invalid email log message.</summary>
        private const string LogInvalidEmail = "[Email] is not a valid email address.";

        /// <summary>Null password log message.</summary>
        private const string LogNullPassword = "[Password] cannot be null.";

        /// <summary>Too short or too long password log message.</summary>
        private const string LogTooLongOrTooShortPassword = "[Password] must be between 8 and 20 characters.";

        /// <summary>Invalid password log message.</summary>
        private const string LogInvalidPassword = "[Password] must contain at least one number, one uppercase letter, one lowercase letter and one symbol.";

        /// <summary>Test for valid user.</summary>
        [Test]
        public void ValidUser()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>Test for invalid user (user with no data).</summary>
        [Test]
        public void InvalidUser_EmptyUser()
        {
            User user = new User();

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(5, results.Count);
            Assert.AreEqual(LogNullFirstName, results[0].ErrorMessage);
            Assert.AreEqual(LogNullLastName, results[1].ErrorMessage);
            Assert.AreEqual(LogNullUserName, results[2].ErrorMessage);
            Assert.AreEqual(LogNullEmail, results[3].ErrorMessage);
            Assert.AreEqual(LogNullPassword, results[4].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with null first name).</summary>
        [Test]
        public void InvalidUser_FirstName_Null()
        {
            User user = new User(null, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullFirstName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with empty first name).</summary>
        [Test]
        public void InvalidUser_FirstName_Empty()
        {
            User user = new User(string.Empty, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullFirstName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with first name too long).</summary>
        [Test]
        public void InvalidUser_FirstName_TooLong()
        {
            User user = new User('X' + new string('x', 16), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongFirstName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with first name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidUser_FirstName_NoUpperCaseLetter()
        {
            User user = new User(new string('x', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidFirstName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with first name that only has uppercase letters).</summary>
        [Test]
        public void InvalidUser_FirstName_NoLowerCaseLetters()
        {
            User user = new User(new string('X', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidFirstName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with first name that contains symbols).</summary>
        [Test]
        public void InvalidUser_FirstName_ContainsSymbol()
        {
            User user = new User("Adr!an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidFirstName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with first name that contains numbers).</summary>
        [Test]
        public void InvalidUser_FirstName_ContainsNumber()
        {
            User user = new User("Adr1an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidFirstName, results[0].ErrorMessage);
        }








        /// <summary>Test for invalid user (user with null last name).</summary>
        [Test]
        public void InvalidUser_LastName_Null()
        {
            User user = new User("Adrian", null, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullLastName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with empty last name).</summary>
        [Test]
        public void InvalidUser_LastName_Empty()
        {
            User user = new User("Adrian", string.Empty, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullLastName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with last name too long).</summary>
        [Test]
        public void InvalidUser_LastName_TooLong()
        {
            User user = new User("Adrian", 'X' + new string('x', 16), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongLastName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with last name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidUser_LastName_NoUpperCaseLetter()
        {
            User user = new User("Adrian", new string('x', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidLastName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with last name that only has uppercase letters).</summary>
        [Test]
        public void InvalidUser_LastName_NoLowerCaseLetters()
        {
            User user = new User("Adrian", new string('X', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidLastName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with last name that contains symbols).</summary>
        [Test]
        public void InvalidUser_LastName_ContainsSymbol()
        {
            User user = new User("Adrian", "Mate!", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidLastName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with last name that contains numbers).</summary>
        [Test]
        public void InvalidUser_LastName_ContainsNumber()
        {
            User user = new User("Adrian", "Mat3i", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidLastName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with null username).</summary>
        [Test]
        public void InvalidUser_UserName_Null()
        {
            User user = new User("Adrian", "Matei", null, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullUserName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with empty username).</summary>
        [Test]
        public void InvalidUser_UserName_Empty()
        {
            User user = new User("Adrian", "Matei", string.Empty, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullUserName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with username too long).</summary>
        [Test]
        public void InvalidUser_UserName_TooLong()
        {
            User user = new User("Adrian", "Matei", new string('x', 31), "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongUserName, results[0].ErrorMessage);
        }

        /// <summary>Test for valid user (user with null phone number).</summary>
        [Test]
        public void ValidUser_PhoneNumber_Null()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", null, "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>Test for invalid user (user with empty phone number).</summary>
        [Test]
        public void InvalidUser_UserValidation_PhoneNumber_Empty()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", string.Empty, "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(LogInvalidPhoneNumber, results[0].ErrorMessage);
            Assert.AreEqual(LogTooLongPhoneNumber, results[1].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with phone number too long).</summary>
        [Test]
        public void InvalidUser_PhoneNumber_TooLong()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", new string('8', 16), "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongPhoneNumber, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with invalid phone number).</summary>
        [Test]
        public void InvalidUser_PhoneNumber_Invalid()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "abc", "adrian.matei@FakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidPhoneNumber, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with null email address).</summary>
        [Test]
        public void InvalidUser_Email_Null()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", null, "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with empty email address).</summary>
        [Test]
        public void InvalidUser_Email_Empty()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", string.Empty, "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with email address too long).</summary>
        [Test]
        public void InvalidUser_Email_TooLong()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", new string('x', 30) + '@' + new string('x', 30), "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with invalid email address).</summary>
        [Test]
        public void InvalidUser_Email_Invalid()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.mateiFakeEmail.com", "P@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with null password).</summary>
        [Test]
        public void InvalidUser_Password_Null()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", null);

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with empty password).</summary>
        [Test]
        public void InvalidUser_Password_Empty()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", string.Empty);

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with password too short).</summary>
        [Test]
        public void InvalidUser_Password_TooShort()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongOrTooShortPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with password too long).</summary>
        [Test]
        public void InvalidUser_Password_TooLong()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1" + new string('x', 20));

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongOrTooShortPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with password that doesn't contain uppercase letters).</summary>
        [Test]
        public void InvalidUser_Password_MissingUpperCaseLetter()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "p@ssword123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with password that doesn't contain lowercase letters).</summary>
        [Test]
        public void InvalidUser_Password_MissingLowerCaseLetter()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@SSWORD123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with password that doesn't contain numbers).</summary>
        [Test]
        public void InvalidUser_Password_MissingNumber()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with password that doesn't contain symbols).</summary>
        [Test]
        public void InvalidUser_Password_MissingSymbol()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "Password123");

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(user, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidPassword, results[0].ErrorMessage);
        }
    }
}
