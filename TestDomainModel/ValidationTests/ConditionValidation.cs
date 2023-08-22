// <copyright file="ConditionValidation.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ModelValidationTests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Models;
    using NUnit.Framework;

    /// <summary>Test class for <see cref="Condition"/> validation.</summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class ConditionValidation
    {
        /// <summary>Null name log message.</summary>
        private const string LogNullName = "[Name] cannot be null.";

        /// <summary>Too long name log message.</summary>
        private const string LogTooLongName = "[Name] must be between 1 and 15 characters.";

        /// <summary>Null description log message.</summary>
        private const string LogNullDescription = "[Description] cannot be null.";

        /// <summary>Too long description log message.</summary>
        private const string LogTooLongDescription = "[Description] must be between 1 and 100 characters.";

        /// <summary>Test for valid condition.</summary>
        [Test]
        public void ValidCondition()
        {
            Condition condition = new Condition("X", "description", 10);

            var context = new ValidationContext(condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(condition, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>Test for invalid condition (condition with no data).</summary>
        [Test]
        public void InvalidCondition_EmptyCondition()
        {
            Condition condition = new Condition();

            var context = new ValidationContext(condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(condition, context, results, true));
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(LogNullName, results[0].ErrorMessage);
            Assert.AreEqual(LogNullDescription, results[1].ErrorMessage);
        }

        /// <summary>Test for invalid condition (condition with null name).</summary>
        [Test]
        public void InvalidCondition_Name_Null()
        {
            Condition condition = new Condition(null, "description", 10);

            var context = new ValidationContext(condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(condition, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid condition (condition with empty name).</summary>
        [Test]
        public void InvalidCondition_Name_Empty()
        {
            Condition condition = new Condition(string.Empty, "description", 10);

            var context = new ValidationContext(condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(condition, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid condition (condition with name too long).</summary>
        [Test]
        public void InvalidCondition_Name_TooLong()
        {
            Condition condition = new Condition(new string('x', 16), "description", 10);

            var context = new ValidationContext(condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(condition, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid condition (condition with null description).</summary>
        [Test]
        public void InvalidCondition_Description_Null()
        {
            Condition condition = new Condition("X", null, 10);

            var context = new ValidationContext(condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(condition, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullDescription, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid condition (condition with empty description).</summary>
        [Test]
        public void InvalidCondition_Description_Empty()
        {
            Condition condition = new Condition("X", string.Empty, 10);

            var context = new ValidationContext(condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(condition, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullDescription, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid condition (condition with description too long).</summary>
        [Test]
        public void InvalidCondition_Description_TooLong()
        {
            Condition condition = new Condition("X", new string('x', 101), 10);

            var context = new ValidationContext(condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(condition, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongDescription, results[0].ErrorMessage);
        }
    }
}
