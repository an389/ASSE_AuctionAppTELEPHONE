// <copyright file="BidValidation.cs" company="Transilvania University of Brasov">
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

    /// <summary>Test class for <see cref="Bid"/> validation.</summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class BidValidation
    {
        /// <summary>Null product log message.</summary>
        private const string LogNullProduct = "[Product] cannot be null.";

        /// <summary>Invalid product log message.</summary>
        private const string LogInvalidProduct = "[Product] must be a valid product.";

        /// <summary>Null buyer log message.</summary>
        private const string LogNullBuyer = "[Buyer] cannot be null.";

        /// <summary>Invalid buyer log message.</summary>
        private const string LogInvalidBuyer = "[Buyer] must be a valid user.";

        /// <summary>Negative amount log message.</summary>
        private const string LogNegativeAmount = "[Amount] must be greater than zero.";

        /// <summary>Test for valid bid.</summary>
        [Test]
        public void ValidBid()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>Test for invalid bid (bid with no data).</summary>
        [Test]
        public void InvalidBid_EmptyBid()
        {
            Bid bid = new Bid();

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(LogNullProduct, results[0].ErrorMessage);
            Assert.AreEqual(LogNullBuyer, results[1].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null product).</summary>
        [Test]
        public void InvalidBid_NullProduct()
        {
            Bid bid = new Bid(
                null,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null product name).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_Name_Null()
        {
            Bid bid = new Bid(
                new Product(
                    null,
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with empty product name).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_Name_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    string.Empty,
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product name too long).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_Name_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    new string('x', 251),
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null product description).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_Description_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    null,
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with empty product description).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_Description_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    string.Empty,
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product description too long).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_Description_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    new string('x', 501),
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null product category).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_Category_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    null,
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null product category name).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_Category_Name_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with empty product category name).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_Category_Name_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(string.Empty, null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product category name too long).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_Category_Name_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(new string('x', 125), null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with negative product starting price).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_StartingPrice_Negative()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    -1,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null product seller).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_NullSeller()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    null,
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null product seller first name).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_FirstName_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User(null, "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with empty product seller first name).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_FirstName_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User(string.Empty, "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller first name too long).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_FirstName_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User('X' + new string('x', 16), "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller first name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_FirstName_NoUpperCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User(new string('x', 10), "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller first name that only has uppercase letters).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_FirstName_NoLowerCaseLetters()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User(new string('X', 10), "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller first name that contains symbols).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_FirstName_ContainsSymbol()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("D!nu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller first name that contains numbers).</summary>
        [Test]
        public void InvalidProduct_InvalidSeller_FirstName_ContainsNumber()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("D1nu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null product seller last name).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_LastName_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", null, "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with empty product seller last name).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_LastName_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", string.Empty, "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller last name too long).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_LastName_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", 'X' + new string('x', 16), "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller last name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_LastName_NoUpperCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", new string('x', 10), "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller last name that only has uppercase letters).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_LastName_NoLowerCaseLetters()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", new string('X', 10), "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller last name that contains symbols).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_LastName_ContainsSymbol()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "G@rbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller last name that contains numbers).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_LastName_ContainsNumber()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "G4rbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null product seller username).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_UserName_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", null, "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with empty product seller username).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_UserName_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", string.Empty, "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller username too long).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_UserName_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", new string('x', 31), "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for valid bid (bid with null product seller phone number).</summary>
        [Test]
        public void ValidBid_ValidProduct_ValidSeller_PhoneNumber_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>Test for invalid bid (bid with empty product seller phone number).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_PhoneNumber_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", string.Empty, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller phone number too long).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_PhoneNumber_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", new string('8', 16), "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for valid bid (bid with invalid product seller phone number).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_PhoneNumber_Invalid()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "abc", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null product seller email address).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_Email_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", null, "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with empty product seller email address).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_Email_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", string.Empty, "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller email address too long).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_Email_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", new string('x', 30) + '@' + new string('x', 30), "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with invalid product seller email address).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_Email_Invalid()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuzFakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null product seller password).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_Password_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", null),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with empty product seller password).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_Password_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", string.Empty),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller password too short).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_Password_TooShort()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "B#b1"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller password too long).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_Password_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "B#b1" + new string('x', 20)),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller password that doesn't contain uppercase letters).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_Password_MissingUpperCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "p@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller password that doesn't contain lowercase letters).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_Password_MissingLowerCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@ROLA123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller password that doesn't contain numbers).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_Password_MissingNumber()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product seller password that doesn't contain symbols).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_InvalidSeller_Password_MissingSymbol()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "Parola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with product auction end date before auction start date).</summary>
        [Test]
        public void InvalidBid_InvalidProduct_EndDate_BeforeStartDate()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null buyer).</summary>
        [Test]
        public void InvalidBid_NullBuyer()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                null,
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null buyer first name).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_FirstName_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User(null, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with empty buyer first name).</summary>
        [Test]
        public void InvalidBid_InvalidBuyerFirstName_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User(string.Empty, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer first name too long).</summary>
        [Test]
        public void InvalidBid_InvalidBuyerFirstName_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User(new string('x', 16), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer first name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidBid_InvalidBuyerFirstName_NoUpperCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User(new string('x', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer first name that only has uppercase letters).</summary>
        [Test]
        public void InvalidBid_InvalidBuyerFirstName_NoLowerCaseLetters()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User(new string('X', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer first name that contains symbols).</summary>
        [Test]
        public void InvalidBid_InvalidBuyerFirstName_ContainsSymbol()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adr!an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer first name that contains numbers).</summary>
        [Test]
        public void InvalidBid_InvalidBuyerFirstName_ContainsNumber()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adr1an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null buyer last name).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_LastName_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", null, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with empty buyer last name).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_LastName_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", string.Empty, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer last name too long).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_LastName_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", new string('x', 16), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer last name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_LastName_NoUpperCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", new string('x', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer last name that only has uppercase letters).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_LastName_NoLowerCaseLetters()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", new string('X', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer last name that contains symbols).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_LastName_ContainsSymbol()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Mate!", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer last name that contains numbers).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_LastName_ContainsNumber()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Mat3i", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null buyer username).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_UserName_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", null, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with empty buyer username).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_UserName_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", string.Empty, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer username too long).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_UserName_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", new string('x', 31), "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for valid bid (bid with null buyer phone number).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_PhoneNumber_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", null, "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>Test for invalid bid (bid with empty buyer phone number).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_PhoneNumber_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", string.Empty, "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer phone number too long).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_PhoneNumber_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", new string('8', 16), "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for valid bid (bid with invalid buyer phone number).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_PhoneNumber_Invalid()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "abc", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null buyer email address).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_Email_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", null, "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with empty buyer email address).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_Email_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", string.Empty, "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer email address too long).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_Email_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", new string('x', 51), "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with invalid buyer email address).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_Email_Invalid()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.mateiFakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with null buyer password).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_Password_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", null),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with empty buyer password).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_Password_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", string.Empty),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer password too short).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_Password_TooShort()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer password too long).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_Password_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1" + new string('x', 20)),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer password that doesn't contain uppercase letters).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_Password_MissingUpperCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "p@ssword123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer password that doesn't contain lowercase letters).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_Password_MissingLowerCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@SSWORD123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer password that doesn't contain numbers).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_Password_MissingNumber()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with buyer password that doesn't contain symbols).</summary>
        [Test]
        public void InvalidBid_InvalidBuyer_Password_MissingSymbol()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "Password123"),
                1000,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidBuyer, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid bid (bid with negative amount).</summary>
        [Test]
        public void InvalidBid_Amount_Negative()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "8888888888", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                -1,
                ECurrency.EUR);

            var context = new ValidationContext(bid, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bid, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeAmount, results[0].ErrorMessage);
        }
    }
}
