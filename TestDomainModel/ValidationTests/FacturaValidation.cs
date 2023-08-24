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
    internal class FacturaValidation
    {
        private const string LogNullEmail = "[ClientEmail] cannot be null.";
        private const string LogTooLongEmail = "[ClientEmail] must have between 5 and 50 digits.";
        private const string LogInvalidEmail = "[ClientEmail] is not a valid email address.";
        [Test]
        public void ValidFacturaValidation()
        {
            Factura factura = new Factura("andrei@fakemail.com", 1.2, 120, false);
            var context = new ValidationContext(factura, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(factura, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void InvalidUser_Email_Null()
        {
            Factura factura = new Factura(null, 1.2, 120, false);
            var context = new ValidationContext(factura, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(factura, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with empty email address).</summary>
        [Test]
        public void InvalidUser_Email_Empty()
        {
            Factura factura = new Factura(string.Empty, 1.2, 120, false);
            var context = new ValidationContext(factura, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(factura, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with email address too long).</summary>
        [Test]
        public void InvalidUser_Email_TooLong()
        {
            Factura factura = new Factura(new string('x', 30) + '@' + new string('x', 30), 1.2, 120, false);
            var context = new ValidationContext(factura, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(factura, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongEmail, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with invalid email address).</summary>
        [Test]
        public void InvalidUser_Email_Invalid()
        {
            Factura factura = new Factura("andreifakemail.com", 1.2, 120, false);
            var context = new ValidationContext(factura, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(factura, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidEmail, results[0].ErrorMessage);
        }
    }
}
