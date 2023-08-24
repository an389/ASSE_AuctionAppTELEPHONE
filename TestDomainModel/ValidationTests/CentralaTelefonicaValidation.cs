using DomainModel.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDomainModel.ValidationTests
{
    internal class CentralaTelefonicaValidation
    {
        private const string LogNullEmail = "[ClientEmail] cannot be null.";
        private const string LogTooLongEmail = "[ClientEmail] must have between 5 and 50 digits.";
        private const string LogInvalidEmail = "[ClientEmail] is not a valid email address.";

        private const string DurataConvorbireNationala = "[DurataConvorbireNationala] cannot be negative.";
        private const string DurataConvorbireInternationala = "[DurataConvorbireInternationala] cannot be negative.";
        private const string DurataConvorbireRetea = "[DurataConvorbireRetea] cannot be negative.";
        private const string SMSNationala = "[SMSNationala] cannot be negative.";
        private const string SMSInternationala = "[SMSInternationala] cannot be negative.";
        private const string SMSRetea = "[SMSRetea] cannot be negative.";
        private const string TraficDeDateNationala = "[TraficDeDateNationala] cannot be negative.";
        private const string TraficDeDateInternationala = "[TraficDeDateInternationala] cannot be negative.";
        private const string TraficDeDateRetea = "[TraficDeDateRetea] cannot be negative.";

        [Test]
        public void ValidCentralaTelefonicaValidation()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void InvalidUser_Email_Null()
        {
            CentralaTelefonica centrala = new CentralaTelefonica(null, DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with empty email address).</summary>
        [Test]
        public void InvalidUser_Email_Empty()
        {
            CentralaTelefonica centrala = new CentralaTelefonica(null, DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with email address too long).</summary>
        [Test]
        public void InvalidUser_Email_TooLong()
        {
            CentralaTelefonica centrala = new CentralaTelefonica(new string('x', 30) + '@' + new string('x', 30), DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with invalid email address).</summary>
        [Test]
        public void InvalidUser_Email_Invalid()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andreifakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidEmail, results[0].ErrorMessage);
        }



        [Test]
        public void InvalidNegativeNumarMinuteNationale()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, -12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(DurataConvorbireNationala, results[0].ErrorMessage);
        }

        [Test]
        public void InvalidNegativeNumarMinuteInternationale()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, -10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(DurataConvorbireInternationala, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeNumarMinuteReteaNegative()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, -9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(DurataConvorbireRetea, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeSMSNationale()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, -2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(SMSNationala, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeSMSInternationale()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, -2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(SMSInternationala, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeSMSRetea()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, -3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(SMSRetea, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeTraficDeDateNationale()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, -44, DateTime.Today, 12, DateTime.Today, 0);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(TraficDeDateNationala, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeTraficDeDateInternationale()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, -12, DateTime.Today, 0);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(TraficDeDateInternationala, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeTraficDeDateRetea()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, -22);

            var context = new ValidationContext(centrala, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(centrala, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(TraficDeDateRetea, results[0].ErrorMessage);
        }
    }
}
