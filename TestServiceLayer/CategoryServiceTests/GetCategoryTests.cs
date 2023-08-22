// <copyright file="GetCategoryTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace CategoryServiceTests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Implementation;

    /// <summary>
    ///     Test class for
    ///         <see cref="CategoryServicesImplementation.GetAllCategories()"/>,
    ///         <see cref="CategoryServicesImplementation.GetCategoryById(int)"/> and
    ///         <see cref="CategoryServicesImplementation.GetCategoryByName(string)"/>
    ///     methods.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class GetCategoryTests
    {
        /// <summary>
        ///     Test for retrieving all existing categories.
        /// </summary>
        [Test]
        public void GET_AllCategories()
        {
            List<Category> categories = GetSampleCategories();

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetAllCategories()).Returns(categories);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            var expected = categories;
            var actual = categoryServices.GetAllCategories();

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Name, actual[i].Name);
                Assert.AreEqual(expected[i].ParentCategory, actual[i].ParentCategory);
            }
        }

        /// <summary>
        ///     Test for retrieving all existing categories but none were found.
        /// </summary>
        [Test]
        public void GET_AllCategories_NoneFound()
        {
            List<Category> emptyCategoryList = new List<Category>();

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetAllCategories()).Returns(emptyCategoryList);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsEmpty(categoryServices.GetAllCategories());
        }

        /// <summary>
        ///     Test for retrieving an existing category with the specified id.
        /// </summary>
        [Test]
        public void GET_CategoryById()
        {
            Category category = GetSampleCategory();

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetCategoryById(category.Id)).Returns(category);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            var expected = category;
            var actual = categoryServices.GetCategoryById(category.Id);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.ParentCategory, actual.ParentCategory);
        }

        /// <summary>
        ///     Test for retrieving an existing category with the specified id but no such category was found.
        /// </summary>
        [Test]
        public void GET_CategoryById_NotFound()
        {
            Category category = GetSampleCategory();
            Category nullCategory = null;

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetCategoryById(category.Id)).Returns(nullCategory);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            var expected = nullCategory;
            var actual = categoryServices.GetCategoryById(category.Id);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///     Test for retrieving an existing category with the specified name.
        /// </summary>
        [Test]
        public void GET_CategoryByName()
        {
            Category category = GetSampleCategory();

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetCategoryByName(category.Name)).Returns(category);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            var expected = category;
            var actual = categoryServices.GetCategoryByName(category.Name);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.ParentCategory, actual.ParentCategory);
        }

        /// <summary>
        ///     Test for retrieving an existing category with the specified name but no such category was found.
        /// </summary>
        [Test]
        public void GET_CategoryByName_NotFound()
        {
            Category category = GetSampleCategory();
            Category nullCategory = null;

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetCategoryByName(category.Name)).Returns(nullCategory);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            var expected = nullCategory;
            var actual = categoryServices.GetCategoryByName(category.Name);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>Gets a sample category.</summary>
        /// <returns>a sample category.</returns>
        private static Category GetSampleCategory()
        {
            return new Category("Aparat foto", null);
        }

        /// <summary>Gets sample categories.</summary>
        /// <returns>a list of sample categories.</returns>
        private static List<Category> GetSampleCategories()
        {
            return new List<Category>
            {
                new Category("Aparat foto", null),
                new Category("Antichitati", null),
            };
        }
    }
}
