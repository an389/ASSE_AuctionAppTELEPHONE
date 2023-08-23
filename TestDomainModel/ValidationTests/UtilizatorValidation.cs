using DomainModel.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDomainModel.ValidationTests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class UtilizatorValidation
    {
        private const string LogNullFirstName = "[FirstName] cannot be null.";
        private const string LogMaximumOrMinimumLenghtUtilizatorFirstName = "[FirstName] must be between 1 and 15 characters.";
        private const string LogRegularExpresionValidUtilizatorFirstName = "[FirstName] must be a valid firstname.";

        private const string LogNullLastName = "[LastName] cannot be null.";
        private const string LogMaximumOrMinimumLenghtUtilizatorLastName = "[LastName] must be between 1 and 15 characters.";
        private const string LogRegularExpresionValidUtilizatorLastName = "[LastName] must be a valid lastname.";

        private const string LogNullEmail = "[Email] cannot be null.";
        private const string LogTooLongEmail = "[Email] must have between 5 and 50 digits.";
        private const string LogInvalidEmail = "[Email] is not a valid email address.";

        private const string LogNullPassword = "[Password] cannot be null.";
        private const string LogTooLongOrTooShortPassword = "[Password] must be between 8 and 20 characters.";
        private const string LogInvalidPassword = "[Password] must contain at least one number, one uppercase letter, one lowercase letter and one symbol.";

        private const string LogNullCNP = "[CNP] cannot be null.";
        private const string LogNegativeCNP = "[CNP] cannot be negative";
        private const string LogIncorectFirstNumberCNP = "Incorect first number from CNP";
        private const string LogIncorectMounthCNP = "[CNP] Incorect month!";
        private const string LogIncorectDayCNP = "[CNP] Incorect day!!";
        private const string LogIncorectDayGT32CNP = "[CNP] Incorect day!!(day cannot be grather than 32";
        private const string LogUnderAgeCNP = "Under AGE!!!";

        private const string LogNullBonusuriID = "[BonusuriId] cannot be null.";
        private const string LogNullEsteBunDePlata = "[EsteBunDePlata] cannot be null.";


      [Test]
        public void ValidUtilizator()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        public void InvalidEmptyUtilizator()
        {
            Utilizator utilizator = new Utilizator();
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(7, results.Count);
            Assert.AreEqual(LogNullFirstName, results[0].ErrorMessage);
            Assert.AreEqual(LogNullLastName, results[1].ErrorMessage);
            Assert.AreEqual(LogNullEmail, results[2].ErrorMessage);
            Assert.AreEqual(LogNullPassword, results[3].ErrorMessage);
            Assert.AreEqual(LogNullCNP, results[4].ErrorMessage);
            Assert.AreEqual(LogNullBonusuriID, results[5].ErrorMessage);
            Assert.AreEqual(LogNullEsteBunDePlata, results[6].ErrorMessage);
        }

        [Test]
        public void NotNullUtilizatorFirstName()
        {
            Utilizator utilizator = new Utilizator(null, "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullFirstName, results[0].ErrorMessage);
        }

        [Test]
        public void EmptyUtilizatorFirstName()
        {
            Utilizator utilizator = new Utilizator(string.Empty, "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullFirstName, results[0].ErrorMessage);
        }

        [Test]
        public void MaximumOrMinimumLenghtUtilizatorFirstName()
        {
            Utilizator utilizator = new Utilizator('X' + new string('x', 16), "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogMaximumOrMinimumLenghtUtilizatorFirstName, results[0].ErrorMessage);
        }

        [Test]
        public void InvalidUser_FirstName_NoUpperCaseLetter()
        {
            Utilizator utilizator = new Utilizator(new string('x', 10), "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogRegularExpresionValidUtilizatorFirstName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with first name that only has uppercase letters).</summary>
        [Test]
        public void InvalidUser_FirstName_NoLowerCaseLetters()
        {
            Utilizator utilizator = new Utilizator(new string('X', 10), "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogRegularExpresionValidUtilizatorFirstName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with first name that contains symbols).</summary>
        [Test]
        public void InvalidUser_FirstName_ContainsSymbol()
        {
            Utilizator utilizator = new Utilizator("Andrei!!", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogRegularExpresionValidUtilizatorFirstName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with first name that contains numbers).</summary>
        [Test]
        public void InvalidUser_FirstName_ContainsNumber()
        {
            Utilizator utilizator = new Utilizator("Andre1", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogRegularExpresionValidUtilizatorFirstName, results[0].ErrorMessage);
        }




        [Test]
        public void InvalidUser_LastName_Null()
        {
            Utilizator utilizator = new Utilizator("Andrei", null, "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullLastName, results[0].ErrorMessage);
        }

        [Test]
        public void InvalidUser_LastName_Empty()
        {
            Utilizator utilizator = new Utilizator("Andrei", string.Empty, "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullLastName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with last name too long).</summary>
        [Test]
        public void InvalidUser_LastName_TooLong()
        {
            Utilizator utilizator = new Utilizator("Andrei", 'X' + new string('x', 16), "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogMaximumOrMinimumLenghtUtilizatorLastName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with last name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidUser_LastName_NoUpperCaseLetter()
        {
            Utilizator utilizator = new Utilizator("Andrei", new string('x', 10), "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogRegularExpresionValidUtilizatorLastName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with last name that only has uppercase letters).</summary>
        [Test]
        public void InvalidUser_LastName_NoLowerCaseLetters()
        {
            Utilizator utilizator = new Utilizator("Andrei", new string('X', 10), "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogRegularExpresionValidUtilizatorLastName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with last name that contains symbols).</summary>
        [Test]
        public void InvalidUser_LastName_ContainsSymbol()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Miha!", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogRegularExpresionValidUtilizatorLastName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with last name that contains numbers).</summary>
        [Test]
        public void InvalidUser_LastName_ContainsNumber()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Miha1", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogRegularExpresionValidUtilizatorLastName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with null email address).</summary>
        [Test]
        public void InvalidUser_Email_Null()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", null, "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with empty email address).</summary>
        [Test]
        public void InvalidUser_Email_Empty()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", string.Empty, "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with email address too long).</summary>
        [Test]
        public void InvalidUser_Email_TooLong()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", new string('x', 30) + '@' + new string('x', 30), "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with invalid email address).</summary>
        [Test]
        public void InvalidUser_Email_Invalid()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andreifakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with null password).</summary>
        [Test]
        public void InvalidUser_Password_Null()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", null, 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with empty password).</summary>
        [Test]
        public void InvalidUser_Password_Empty()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", string.Empty, 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with password too short).</summary>
        [Test]
        public void InvalidUser_Password_TooShort()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "A#a1", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongOrTooShortPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with password too long).</summary>
        [Test]
        public void InvalidUser_Password_TooLong()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "A#a1" + new string('x', 20), 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongOrTooShortPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with password that doesn't contain uppercase letters).</summary>
        [Test]
        public void InvalidUser_Password_MissingUpperCaseLetter()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "p@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with password that doesn't contain lowercase letters).</summary>
        [Test]
        public void InvalidUser_Password_MissingLowerCaseLetter()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@SSWORD123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with password that doesn't contain numbers).</summary>
        [Test]
        public void InvalidUser_Password_MissingNumber()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with password that doesn't contain symbols).</summary>
        [Test]
        public void InvalidUser_Password_MissingSymbol()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "Pssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidPassword, results[0].ErrorMessage);
        }

        /// <summary>Test for valid user (user with null phone number).</summary>
        [Test]
        public void NegativeCNP()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", -5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeCNP, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with empty phone number).</summary>
        [Test]
        public void IncorectFirstNumberCNP()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 4000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogIncorectFirstNumberCNP, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with phone number too long).</summary>
        [Test]
        public void IncorectMonthCNP()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5002430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogIncorectMounthCNP, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with invalid phone number).</summary>
        [Test]
        public void IncorectDayCNP()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000450385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogIncorectDayCNP, results[0].ErrorMessage);
        }



        /// <summary>Test for valid user (user with null phone number).</summary>
        [Test]
        public void IncorectDayGratherThan32CNP()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000435385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            
            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogIncorectDayGT32CNP, results[0].ErrorMessage);
        }
        
        [Test]
        public void UnderAgeCNP()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5200430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogUnderAgeCNP, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with empty phone number).</summary>
        [Test]
        public void NullBonusuri()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, null);
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullBonusuriID, results[0].ErrorMessage);
        }



        /// <summary>Test for valid user (user with null phone number).</summary>
        [Test]
        public void EmptyBonusuri()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, string.Empty);
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullBonusuriID, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with empty phone number).</summary>
        [Test]
        public void NullEsteBunDePlata()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var context = new ValidationContext(utilizator, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(utilizator, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

    }
}
