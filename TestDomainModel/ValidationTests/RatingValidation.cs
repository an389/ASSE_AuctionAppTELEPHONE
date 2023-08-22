// <copyright file="RatingValidation.cs" company="Transilvania University of Brasov">
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

    /// <summary>
    ///     Test class for <see cref="Rating"/> validation.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class RatingValidation
    {
        /// <summary>Null product log message.</summary>
        private const string LogNullProduct = "[Product] cannot be null.";

        /// <summary>Invalid product log message.</summary>
        private const string LogInvalidProduct = "[Product] must be a valid product.";

        /// <summary>Null rating user log message.</summary>
        private const string LogNullRatingUser = "[RatingUser] cannot be null.";

        /// <summary>Invalid rating user log message.</summary>
        private const string LogInvalidRatingUser = "[RatingUser] must be a valid user.";

        /// <summary>Null rated user log message.</summary>
        private const string LogNullRatedUser = "[RatedUser] cannot be null.";

        /// <summary>Invalid rated user log message.</summary>
        private const string LogInvalidRatedUser = "[RatedUser] must be a valid user.";

        /// <summary>Invalid grade log message.</summary>
        private const string LogInvalidGrade = "[Grade] must be between 1 and 10.";

        /// <summary>Test for valid rating.</summary>
        [Test]
        public void ValidRating()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>Test for invalid rating (rating with no data).</summary>
        [Test]
        public void InvalidRating_EmptyRating()
        {
            Rating rating = new Rating();

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(4, results.Count);
            Assert.AreEqual(LogNullProduct, results[0].ErrorMessage);
            Assert.AreEqual(LogNullRatingUser, results[1].ErrorMessage);
            Assert.AreEqual(LogNullRatedUser, results[2].ErrorMessage);
            Assert.AreEqual(LogInvalidGrade, results[3].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null product).</summary>
        [Test]
        public void InvalidRating_NullProduct()
        {
            Rating rating = new Rating(
                null,
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null product name).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_Name_Null()
        {
            Rating rating = new Rating(
                new Product(
                    null,
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty product name).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_Name_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    string.Empty,
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product name too long).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_Name_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    new string('x', 251),
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null product description).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_Description_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    null,
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty product description).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_Description_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    string.Empty,
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product description too long).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_Description_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    new string('x', 501),
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null product category).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_Category_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    null,
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null product category name).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidCategory_Name_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty product category name).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidCategory_Name_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(string.Empty, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product category name too long).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidCategory_Name_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(new string('x', 101), null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with negative product starting price).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_StartingPrice_Negative()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    -1,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null product seller).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_NullSeller()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    null,
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null product seller first name).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_FirstName_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User(null, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty product seller first name).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_FirstName_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User(string.Empty, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller first name too long).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_FirstName_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User('X' + new string('x', 16), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller first name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_FirstName_NoUpperCaseLetter()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User(new string('x', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller first name that only has uppercase letters).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_FirstName_NoLowerCaseLetters()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User(new string('X', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller first name that contains symbols).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_FirstName_ContainsSymbol()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adr!an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller first name that contains numbers).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_FirstName_ContainsNumber()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adr1an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null product seller last name).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_LastName_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", null, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty product seller last name).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_LastName_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", string.Empty, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller last name too long).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_LastName_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", 'X' + new string('x', 16), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller last name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_LastName_NoUpperCaseLetter()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", new string('x', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller last name that only has uppercase letters).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_LastName_NoLowerCaseLetters()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", new string('X', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller last name that contains symbols).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_LastName_ContainsSymbol()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Mate!", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller last name that contains numbers).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_LastName_ContainsNumber()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Mat3i", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null product seller username).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_UserName_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", null, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty product seller username).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_UserName_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", string.Empty, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller username too long).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_UserName_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", new string('x', 31), "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for valid rating (rating with null product seller phone number).</summary>
        [Test]
        public void ValidRating_InvalidProduct_InvalidSeller_PhoneNumber_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", null, "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>Test for invalid rating (rating with empty product seller phone number).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_PhoneNumber_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", string.Empty, "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller phone number too long).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_PhoneNumber_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", new string('8', 16), "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for valid rating (rating with invalid product seller phone number).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_PhoneNumber_Invalid()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "abc", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null product seller email address).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_Email_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", null, "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty product seller email address).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_Email_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", string.Empty, "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller email address too long).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_Email_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", new string('x', 30) + '@' + new string('x', 30), "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with invalid product seller email address).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_Email_Invalid()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.mateiFakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null product seller password).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_Password_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", null),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty product seller password).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_Password_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", string.Empty),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller password too short).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_Password_TooShort()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller password too long).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_Password_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1" + new string('x', 20)),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller password that doesn't contain uppercase letters).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_Password_MissingUpperCaseLetter()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "p@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller password that doesn't contain lowercase letters).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_Password_MissingLowerCaseLetter()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@SSWORD123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller password that doesn't contain numbers).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_Password_MissingNumber()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product seller password that doesn't contain symbols).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_InvalidSeller_Password_MissingSymbol()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "Password123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with product auction end date before auction start date).</summary>
        [Test]
        public void InvalidRating_InvalidProduct_EndDate_BeforeStartDate()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "Password123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidProduct, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null rating user).</summary>
        [Test]
        public void InvalidRating_NullRatingUser()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                null,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null rating user first name).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_FirstName_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User(null, "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty rating user first name).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_FirstName_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User(string.Empty, "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user first name too long).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_FirstName_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User('X' + new string('x', 16), "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user first name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_FirstName_NoUpperCaseLetter()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User(new string('x', 10), "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user first name that only has uppercase letters).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_FirstName_NoLowerCaseLetters()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User(new string('X', 10), "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user first name that contains symbols).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_FirstName_ContainsSymbol()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("D!nu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user first name that contains numbers).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_FirstName_ContainsNumber()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("D1nu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null rating user last name).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_LastName_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", null, "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty rating user last name).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_LastName_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", string.Empty, "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user last name too long).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_LastName_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", 'X' + new string('x', 16), "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user last name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_LastName_NoUpperCaseLetters()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", new string('x', 10), "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user last name that only has uppercase letters).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_LastName_NoLowerCaseLetters()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", new string('X', 10), "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user last name that contains symbols).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_LastName_ContainsSymbol()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "G@rbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user last name that contains numbers).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_LastName_ContainsNumber()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "G4rbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null rating user username).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_UserName_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", null, null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty rating user username).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_UserName_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", string.Empty, null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user username too long).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_UserName_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", new string('x', 31), null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for valid rating (rating with null rating user phone number).</summary>
        [Test]
        public void ValidRating_ValidRatingUser_PhoneNumber_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>Test for invalid rating (rating with empty rating user phone number).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_PhoneNumber_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", string.Empty, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user phone number too long).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_PhoneNumber_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", new string('8', 16), "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with invalid rating user phone number).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_PhoneNumber_Invalid()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", "abc", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null rating user email address).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_Email_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, null, "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty rating user email address).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_Email_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, string.Empty, "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user email address too long).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_Email_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, new string('x', 30) + '@' + new string('x', 30), "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with invalid rating user email address).</summary>
        [Test]
        public void InvalidRating_InvaliRatingUser_Email_Invalid()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuzFakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null rating user password).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_Password_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", null),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty rating user password).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_Password_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", string.Empty),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user password too short).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_Password_TooShort()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "A#a1"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user password too long).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_Password_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "A#a1" + new string('x', 20)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user password that doesn't contain uppercase letters).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_Password_MissingUpperCaseLetter()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "p@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user password that doesn't contain lowercase letters).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_Password_MissingLowerCaseLetter()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@ROLA123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user password that doesn't contain numbers).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_Password_MissingNumber()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rolaP@rola"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rating user password that doesn't contain symbols).</summary>
        [Test]
        public void InvalidRating_InvalidRatingUser_Password_MissingSymbol()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "Parola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatingUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null rated user).</summary>
        [Test]
        public void InvalidRating_RatedUser_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                null,
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null rated user first name).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_FirstName_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User(null, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty rated user first name).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_FirstName_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User(string.Empty, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user first name too long).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_FirstName_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User('X' + new string('x', 16), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user first name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_FirstName_NoUpperCaseLetters()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User(new string('x', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user first name that only has uppercase letters).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_FirstName_NoLowerCaseLetters()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User(new string('X', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user first name that contains symbols).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_FirstName_ContainsSymbol()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adr!an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user first name that contains numbers).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_FirstName_ContainsNumber()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adr1an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null rated user last name).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_LastName_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", null, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty rated user last name).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_LastName_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", string.Empty, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user last name too long).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_LastName_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", 'X' + new string('x', 16), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user last name that doesn't start with an uppercase letter).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_LastName_NoUpperCaseLetters()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", new string('x', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user last name that only has uppercase letters).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_LastName_NoLowerCaseLetters()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", new string('X', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user last name that contains symbols).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_LastName_ContainsSymbol()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Mate!", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user last name that contains numbers).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_LastName_ContainsNumber()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Mat3i", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null rated user username).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_UserName_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", null, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty rated user username).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_UserName_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", string.Empty, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user username too long).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_UserName_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", new string('x', 31), "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for valid rating (rating with null rated user phone number).</summary>
        [Test]
        public void ValidRating_ValidRatedUser_PhoneNumber_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", null, "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        /// <summary>Test for invalid rating (rating with empty rated user phone number).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_PhoneNumber_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", string.Empty, "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user phone number too long).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_PhoneNumber_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", new string('x', 16), "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with invalid rated user phone number).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_PhoneNumber_Invalid()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "abc", "adrian.matei@FakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null rated user email address).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_Email_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", null, "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty rated user email address).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_Email_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", string.Empty, "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user email address too long).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_Email_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", new string('x', 51), "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with invalid rated user email address).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_Email_Invalid()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.mateiFakeEmail.com", "P@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with null rated user password).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_Password_Null()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", null),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with empty rated user password).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_Password_Empty()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", string.Empty),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user password too short).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_Password_TooShort()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user password too long).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_Password_TooLong()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1" + new string('x', 20)),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user password that doesn't contain uppercase letters).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_Password_MissingUpperCaseLetter()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "p@ssword123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user password that doesn't contain lowercase letters).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_Password_MissingLowerCaseLetter()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@SSWORD123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user password that doesn't contain numbers).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_Password_MissingNumber()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with rated user password that doesn't contain symbols).</summary>
        [Test]
        public void InvalidRating_InvalidRatedUser_Password_MissingSymbol()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "Password123"),
                7);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidRatedUser, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with grade less than 1).</summary>
        [Test]
        public void InvalidRating_Grade_LessThan1()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                0);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidGrade, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid rating (rating with grade more than 10).</summary>
        [Test]
        public void InvalidRating_Grade_MoreThan10()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                11);

            var context = new ValidationContext(rating, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(rating, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogInvalidGrade, results[0].ErrorMessage);
        }
    }
}
