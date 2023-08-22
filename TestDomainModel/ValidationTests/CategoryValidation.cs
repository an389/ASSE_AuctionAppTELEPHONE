// <copyright file="CategoryValidation.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ModelValidationTests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Models;
    using NUnit.Framework;

    /// <summary>Test class for <see cref="Category"/> validation.</summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class CategoryValidation
    {
        /// <summary>Null name log message.</summary>
        private const string LogNullName = "[Name] cannot be null.";

        /// <summary>Too long name log message.</summary>
        private const string LogTooLongName = "[Name] must be between 1 and 100 characters.";

        /// <summary>Test for valid category.</summary>
        [Test]
        public void ValidCategory()
        {
            Category category = new Category("Aparat foto", new Category("TV, Audio-Video & Foto", null));

            var context = new ValidationContext(category, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(category, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>Test for invalid category (category with no data).</summary>
        [Test]
        public void InvalidCategory_EmptyCategory()
        {
            Category category = new Category();

            var context = new ValidationContext(category, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(category, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid category (category with null name).</summary>
        [Test]
        public void InvalidCategory_Name_Null()
        {
            Category category = new Category(null, new Category("TV, Audio-Video & Foto", null));

            var context = new ValidationContext(category, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(category, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid category (category with empty name).</summary>
        [Test]
        public void InvalidCategory_Name_Empty()
        {
            Category category = new Category(string.Empty, new Category("TV, Audio-Video & Foto", null));

            var context = new ValidationContext(category, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(category, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid category (category with name too long).</summary>
        [Test]
        public void InvalidCategory_Name_TooLong()
        {
            Category category = new Category(new string('x', 101), new Category("TV, Audio-Video & Foto", null));

            var context = new ValidationContext(category, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(category, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid category (category with null parent category).</summary>
        [Test]
        public void ValidCategory_ParentCategory_Null()
        {
            Category category = new Category("Aparat foto", null);

            var context = new ValidationContext(category, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(category, context, results, true));
            Assert.AreEqual(0, results.Count);
        }
    }
}
