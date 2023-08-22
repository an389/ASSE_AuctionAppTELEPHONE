// <copyright file="GetProductTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ProductServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Enums;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Implementation;

    /// <summary>
    ///     Test class for
    ///         <see cref="ProductServicesImplementation.GetAllProducts()"/> and
    ///         <see cref="ProductServicesImplementation.GetProductById(int)"/>
    ///     methods.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class GetProductTests
    {
        /// <summary>
        ///     Test for retrieving all existing products.
        /// </summary>
        [Test]
        public void GET_AllProducts()
        {
            List<Product> products = GetSampleProducts();

            var productServiceMock = new Mock<IProductDataServices>();
            productServiceMock.Setup(x => x.GetAllProducts()).Returns(products);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            var expected = products;
            var actual = productServices.GetAllProducts();

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Name, actual[i].Name);
                Assert.AreEqual(expected[i].Description, actual[i].Description);

                Assert.AreEqual(expected[i].Category.Id, actual[i].Category.Id);
                Assert.AreEqual(expected[i].Category.Name, actual[i].Category.Name);
                Assert.AreEqual(expected[i].Category.ParentCategory, actual[i].Category.ParentCategory);

                Assert.AreEqual(expected[i].StartingPrice, actual[i].StartingPrice);
                Assert.AreEqual(expected[i].Currency, actual[i].Currency);
                Assert.AreEqual(expected[i].CreationDate, actual[i].CreationDate);
                Assert.AreEqual(expected[i].StartDate, actual[i].StartDate);
                Assert.AreEqual(expected[i].EndDate, actual[i].EndDate);
                Assert.AreEqual(expected[i].TerminationDate, actual[i].TerminationDate);
            }
        }

        /// <summary>
        ///     Test for retrieving all existing products but none were found.
        /// </summary>
        [Test]
        public void GET_AllProducts_NoneFound()
        {
            List<Product> emptyProductList = new List<Product>();

            var productServiceMock = new Mock<IProductDataServices>();
            productServiceMock.Setup(x => x.GetAllProducts()).Returns(emptyProductList);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(productServices.GetAllProducts());
        }

        /// <summary>
        ///     Test for retrieving an existing product with the specified id.
        /// </summary>
        [Test]
        public void GET_ProductById()
        {
            Product product = GetSampleProduct();

            var productServiceMock = new Mock<IProductDataServices>();
            productServiceMock.Setup(x => x.GetProductById(product.Id)).Returns(product);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            var expected = product;
            var actual = productServices.GetProductById(product.Id);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);

            Assert.AreEqual(expected.Category.Id, actual.Category.Id);
            Assert.AreEqual(expected.Category.Name, actual.Category.Name);
            Assert.AreEqual(expected.Category.ParentCategory, actual.Category.ParentCategory);

            Assert.AreEqual(expected.StartingPrice, actual.StartingPrice);
            Assert.AreEqual(expected.Currency, actual.Currency);
            Assert.AreEqual(expected.CreationDate, actual.CreationDate);
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.EndDate, actual.EndDate);
            Assert.AreEqual(expected.TerminationDate, actual.TerminationDate);
        }

        /// <summary>
        ///     Test for retrieving an existing product with the specified id but no such product was found.
        /// </summary>
        [Test]
        public void GET_ProductById_NotFound()
        {
            Product product = GetSampleProduct();
            Product nullProduct = null;

            var productServiceMock = new Mock<IProductDataServices>();
            productServiceMock.Setup(x => x.GetProductById(product.Id)).Returns(nullProduct);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var productServices = new ProductServicesImplementation(productServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            var expected = nullProduct;
            var actual = productServices.GetProductById(product.Id);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>Gets a sample product.</summary>
        /// <returns>a sample product.</returns>
        private static Product GetSampleProduct()
        {
            return new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));
        }

        /// <summary>Gets sample products.</summary>
        /// <returns>a list of sample products.</returns>
        private static List<Product> GetSampleProducts()
        {
            List<Product> products = new List<Product>();

            Product product_1 = new Product(
                "Aparat foto CANNON",
                "face poze",
                new Category("Aparat foto", null),
                100,
                ECurrency.EUR,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            Product product_2 = new Product(
                "Vaza ming",
                "foarte veche",
                new Category("Antichitati", null),
                1000000,
                ECurrency.JPY,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(10));

            products.Add(product_1);
            products.Add(product_2);

            return products;
        }
    }
}
