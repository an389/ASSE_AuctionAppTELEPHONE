// <copyright file="ProductValidation.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ModelValidationTests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Enums;
    using DomainModel.Models;
    using NUnit.Framework;

    /// <summary>Test class for <see cref="Product"/> validation.</summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class ProductValidation
    {
        /// <summary>Null name log message.</summary>
        private const string LogNullName = "[Name] cannot be null.";

        /// <summary>Too long name log message.</summary>
        private const string LogTooLongName = "[Name] must be between 1 and 250 characters.";

        /// <summary>Null description log message.</summary>
        private const string LogNullDescription = "[Description] cannot be null.";

        /// <summary>Too long description log message.</summary>
        private const string LogTooLongDescription = "[Description] must be between 1 and 500 characters.";

        /// <summary>Null category log message.</summary>
        private const string LogNullCategory = "[Category] cannot be null.";

        /// <summary>Invalid category log message.</summary>
        private const string LogInvalidCategory = "[Category] must be a valid category.";

        /// <summary>Null seller log message.</summary>
        private const string LogNullSeller = "[Seller] cannot be null.";

        /// <summary>Invalid seller log message.</summary>
        private const string LogInvalidSeller = "[Seller] must be a valid user.";

        /// <summary>End date before start date log message.</summary>
        private const string LogEndDateBeforeStartDate = "[EndDate] cannot be before [StartDate].";

        /// <summary>Negative starting price log message.</summary>
        private const string LogNegativeStartingPrice = "[StartingPrice] cannot be negative.";

        /// <summary>Test for valid product.</summary>
        [Test]
        public void ValidProduct()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>Test for invalid product (product with no data).</summary>
        [Test]
        public void InvalidProduct_EmptyProduct()
        {
            Product product = new Product();

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(5, results.Count);
            Assert.AreEqual(LogNullName, results[0].ErrorMessage);
            Assert.AreEqual(LogNullDescription, results[1].ErrorMessage);
            Assert.AreEqual(LogNullCategory, results[2].ErrorMessage);
            Assert.AreEqual(LogNullSeller, results[3].ErrorMessage);
            Assert.AreEqual(LogEndDateBeforeStartDate, results[4].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with null name).</summary>
        [Test]
        public void InvalidProduct_Name_Null()
        {
            Product product = new Product(
                null,
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with empty name).</summary>
        [Test]
        public void InvalidProduct_Name_Empty()
        {
            Product product = new Product(
                string.Empty,
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with name too long).</summary>
        [Test]
        public void InvalidProduct_Name_TooLong()
        {
            Product product = new Product(
                new string('x', 251),
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongName, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with null description).</summary>
        [Test]
        public void InvalidProduct_Description_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                null,
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullDescription, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with empty description).</summary>
        [Test]
        public void InvalidProduct_Description_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                string.Empty,
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullDescription, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with description too long).</summary>
        [Test]
        public void InvalidProduct_Description_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                new string('x', 501),
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongDescription, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with null category).</summary>
        [Test]
        public void InvalidProduct_Category_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                null,
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullCategory, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with null category name).</summary>
        [Test]
        public void InvalidProduct_InvalidCategory_Name_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category(null, null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidCategory, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with empty category name).</summary>
        [Test]
        public void InvalidProduct_InvalidCategory_Name_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category(string.Empty, null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidCategory, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with category name too long).</summary>
        [Test]
        public void InvalidProduct_InvalidCategory_Name_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category(new string('x', 101), null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidCategory, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with negative starting price).</summary>
        [Test]
        public void InvalidProduct_StartingPrice_Negative()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                -1,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeStartingPrice, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with null seller).</summary>
        [Test]
        public void InvalidProduct_NullSeller()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                null,
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with null seller first name).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_FirstName_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User(null, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with empty seller first name).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_FirstName_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User(string.Empty, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller first name too long).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_FirstName_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User('X' + new string('x', 16), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller first name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_FirstName_NoUpperCaseLetter()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User(new string('x', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller first name that only has uppercase letters).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_FirstName_NoLowerCaseLetters()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User(new string('X', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller first name that contains symbols).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_FirstName_ContainsSymbol()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adr!an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller first name that contains numbers).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_FirstName_ContainsNumber()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adr1an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with null seller last name).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_LastName_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", null, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with empty seller last name).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_LastName_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", string.Empty, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller last name too long).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_LastName_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", 'X' + new string('x', 16), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller last name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_LastName_NoUpperCaseLetter()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", new string('x', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller last name that only has uppercase letters).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_LastName_NoLowerCaseLetters()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", new string('X', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller last name that contains symbols).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_LastName_ContainsSymbol()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Mate!", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller last name that contains numbers).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_LastName_ContainsNumber()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Mat3i", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with null seller username).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_UserName_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", null, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with empty seller username).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_UserName_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", string.Empty, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller username too long).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_UserName_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", new string('x', 31), "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for valid product (product with null seller phone number).</summary>
        [Test]
        public void ValidProduct_ValidSeller_PhoneNumber_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", null, "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>Test for invalid product (product with empty seller phone number).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_PhoneNumber_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", string.Empty, "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller phone number too long).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_PhoneNumber_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", new string('8', 16), "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with invalid seller phone number).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_PhoneNumber_Invalid()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "abc", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with null seller email address).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_Email_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", null, "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with empty seller email address).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_Email_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", string.Empty, "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller email address too long).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_Email_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", new string('x', 30) + '@' + new string('x', 30), "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with invalid seller email address).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_Email_Invalid()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.mateiFakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with null seller password).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_Password_Null()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", null),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with empty seller password).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_Password_Empty()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", string.Empty),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller password too short).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_Password_TooShort()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller password too long).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_Password_TooLong()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1" + new string('x', 20)),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller password that doesn't contain uppercase letters).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_Password_MissingUpperCaseLetter()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "p@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller password that doesn't contain lowercase letters).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_Password_MissingLowerCaseLetter()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@SSWORD123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller password that doesn't contain numbers).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_Password_MissingNumber()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with seller password that doesn't contain symbols).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_Password_MissingSymbol()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "Password123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidSeller, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid product (product with auction end date before auction start date).</summary>
        [Test]
        public void InvalidProduct_EndDate_BeforeStartDate()
        {
            Product product = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today);

            var context = new ValidationContext(product, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(product, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogEndDateBeforeStartDate, results[0].ErrorMessage);
        }
    }
}
