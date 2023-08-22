// <copyright file="UpdateCategoryTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace CategoryServiceTests
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Implementation;

    /// <summary>
    ///     Test class for <see cref="CategoryServicesImplementation.UpdateCategory(Category)"/> method.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class UpdateCategoryTests
    {
        /// <summary>Null category log message.</summary>
        private const string LogUpdateNullCategory = "Attempted to update a null category.";

        /// <summary>Invalid category log message.</summary>
        private const string LogUpdateInvalidCategory = "Attempted to update an invalid category.";

        /// <summary>Non-existing category log message.</summary>
        private const string LogUpdateNonexistingCategory = "Attempted to update a nonexisting category.";

        /// <summary>Existing duplicate category log message.</summary>
        private const string LogUpdateExistingCategoryName = "Attempted to update a category by changing the name to an existing category name.";

        /// <summary>
        ///     Test for updating a null category.
        /// </summary>
        [Test]
        public void UPDATE_NullCategory()
        {
            Category category = null;

            var serviceMock = new Mock<ICategoryDataServices>();
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(categoryServices.UpdateCategory(category));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNullCategory)));
        }

        /// <summary>
        ///     Test for updating an invalid category (a category with null name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidCategory_Name_Null()
        {
            Category category = new Category(null, new Category("TV, Audio-Video & Foto", null));

            var serviceMock = new Mock<ICategoryDataServices>();
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(categoryServices.UpdateCategory(category));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidCategory))));
        }

        /// <summary>
        ///     Test for updating an invalid category (a category with empty name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidCategory_Name_Empty()
        {
            Category category = new Category(string.Empty, new Category("TV, Audio-Video & Foto", null));

            var serviceMock = new Mock<ICategoryDataServices>();
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(categoryServices.UpdateCategory(category));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidCategory))));
        }

        /// <summary>
        ///     Test for updating an invalid category (a category with name too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidCategory_Name_TooLong()
        {
            Category category = new Category(new string('x', 101), new Category("TV, Audio-Video & Foto", null));

            var serviceMock = new Mock<ICategoryDataServices>();
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(categoryServices.UpdateCategory(category));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidCategory))));
        }

        /// <summary>
        ///     Test for updating an invalid category (a category with null parent category).
        /// </summary>
        [Test]
        public void UPDATE_ValidCategory_ParentCategory_Null()
        {
            Category category = new Category("Aparat foto", null);
            Category nullCategory = null;

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetCategoryById(category.Id)).Returns(category);
            serviceMock.Setup(x => x.GetCategoryByName(category.Name)).Returns(nullCategory);
            serviceMock.Setup(x => x.UpdateCategory(category)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsTrue(categoryServices.UpdateCategory(category));
        }

        /// <summary>
        ///     Test for updating a non-existing category.
        /// </summary>
        [Test]
        public void UPDATE_NonExistingCategory()
        {
            Category category = new Category("Aparat foto", new Category("TV, Audio-Video & Foto", null));
            Category nullCategory = null;

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetCategoryById(category.Id)).Returns(nullCategory);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(categoryServices.UpdateCategory(category));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNonexistingCategory)));
        }

        /// <summary>
        ///     Test for updating an already existing category name.
        /// </summary>
        [Test]
        public void UPDATE_ValidCategory_ChangeName_ExistingCategoryName()
        {
            Category category = new Category("Aparat foto", new Category("TV, Audio-Video & Foto", null));
            Category category2 = new Category("Aparate foto", new Category("TV, Audio-Video & Foto", null));

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetCategoryById(category2.Id)).Returns(category);
            serviceMock.Setup(x => x.GetCategoryByName(category2.Name)).Returns(category2);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(categoryServices.UpdateCategory(category2));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateExistingCategoryName)));
        }

        /// <summary>
        ///     Test for updating a valid category by changing it's name.
        /// </summary>
        [Test]
        public void UPDATE_ValidCategory_ChangeName()
        {
            Category category = new Category("Aparat foto", new Category("TV, Audio-Video & Foto", null));
            Category category2 = new Category("Aparate foto", new Category("TV, Audio-Video & Foto", null));
            Category nullCategory = null;

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetCategoryById(category2.Id)).Returns(category);
            serviceMock.Setup(x => x.GetCategoryByName(category2.Name)).Returns(nullCategory);
            serviceMock.Setup(x => x.UpdateCategory(category2)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsTrue(categoryServices.UpdateCategory(category2));
        }

        /// <summary>
        ///     Test for updating a valid category.
        /// </summary>
        [Test]
        public void UPDATE_ValidCategory()
        {
            Category category = new Category("Aparat foto", new Category("TV, Audio-Video & Foto", null));
            Category nullCategory = null;

            var serviceMock = new Mock<ICategoryDataServices>();
            serviceMock.Setup(x => x.GetCategoryById(category.Id)).Returns(category);
            serviceMock.Setup(x => x.GetCategoryByName(category.Name)).Returns(nullCategory);
            serviceMock.Setup(x => x.UpdateCategory(category)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var categoryServices = new CategoryServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsTrue(categoryServices.UpdateCategory(category));
        }
    }
}
