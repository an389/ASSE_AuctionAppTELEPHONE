// <copyright file="UpdateConditionTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace ConditionServiceTests
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Implementation;

    /// <summary>
    ///     Test class for <see cref="ConditionServicesImplementation.UpdateCondition(DomainModel.Models.Condition)"/> method.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class UpdateConditionTests
    {
        /// <summary>Null condition log message.</summary>
        private const string LogUpdateNullCondition = "Attempted to update a null condition.";

        /// <summary>Invalid condition log message.</summary>
        private const string LogUpdateInvalidCondition = "Attempted to update an invalid condition.";

        /// <summary>Non-existing condition log message.</summary>
        private const string LogUpdateNonexistingCondition = "Attempted to update a nonexisting condition.";

        /// <summary>Existing duplicate condition log message.</summary>
        private const string LogUpdateExistingConditionName = "Attempted to update a condition by changing the name to an existing condition name.";

        /// <summary>
        ///     Test for updating a null condition.
        /// </summary>
        [Test]
        public void UPDATE_NullCondition()
        {
            Condition condition = null;

            var serviceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.UpdateCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNullCondition)));
        }

        /// <summary>
        ///     Test for updating an invalid condition (a condition with null name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidCondition_Name_Null()
        {
            Condition condition = new Condition(null, "description", 10);

            var serviceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.UpdateCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidCondition))));
        }

        /// <summary>
        ///     Test for updating an invalid condition (a condition with empty name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidCondition_Name_Empty()
        {
            Condition condition = new Condition(string.Empty, "description", 10);

            var serviceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.UpdateCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidCondition))));
        }

        /// <summary>
        ///     Test for updating an invalid condition (a condition with name too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidCondition_Name_TooLong()
        {
            Condition condition = new Condition(new string('x', 16), "description", 10);

            var serviceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.UpdateCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidCondition))));
        }

        /// <summary>
        ///     Test for updating an invalid condition (a condition with null description).
        /// </summary>
        [Test]
        public void UPDATE_InvalidCondition_Description_Null()
        {
            Condition condition = new Condition("X", null, 10);

            var serviceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.UpdateCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidCondition))));
        }

        /// <summary>
        ///     Test for updating an invalid condition (a condition with empty description).
        /// </summary>
        [Test]
        public void UPDATE_InvalidCondition_Description_Empty()
        {
            Condition condition = new Condition("X", string.Empty, 10);

            var serviceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.UpdateCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidCondition))));
        }

        /// <summary>
        ///     Test for updating an invalid condition (a condition with description too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidCondition_Description_TooLong()
        {
            Condition condition = new Condition("X", new string('x', 101), 10);

            var serviceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.UpdateCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidCondition))));
        }

        /// <summary>
        ///     Test for updating a non-existing condition.
        /// </summary>
        [Test]
        public void UPDATE_NonExistingCondition()
        {
            Condition condition = new Condition("X", "x", 42);
            Condition nullCondition = null;

            var serviceMock = new Mock<IConditionDataServices>();
            serviceMock.Setup(x => x.GetConditionById(condition.Id)).Returns(nullCondition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.UpdateCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNonexistingCondition)));
        }

        /// <summary>
        ///     Test for updating an already existing condition name.
        /// </summary>
        [Test]
        public void UPDATE_ValidCondition_ChangeName_ExistingConditionName()
        {
            Condition condition = new Condition("X", "x", 42);
            Condition condition2 = new Condition("Y", "x", 42);

            var serviceMock = new Mock<IConditionDataServices>();
            serviceMock.Setup(x => x.GetConditionById(condition2.Id)).Returns(condition);
            serviceMock.Setup(x => x.GetConditionByName(condition2.Name)).Returns(condition2);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.UpdateCondition(condition2));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateExistingConditionName)));
        }

        /// <summary>
        ///     Test for updating a valid condition by changing it's name.
        /// </summary>
        [Test]
        public void UPDATE_ValidCondition_ChangeName()
        {
            Condition condition = new Condition("X", "x", 42);
            Condition condition2 = new Condition("Y", "x", 42);
            Condition nullCondition = null;

            var serviceMock = new Mock<IConditionDataServices>();
            serviceMock.Setup(x => x.GetConditionById(condition.Id)).Returns(condition);
            serviceMock.Setup(x => x.GetConditionByName(condition2.Name)).Returns(nullCondition);
            serviceMock.Setup(x => x.UpdateCondition(condition2)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsTrue(conditionServices.UpdateCondition(condition2));
        }

        /// <summary>
        ///     Test for updating a valid condition.
        /// </summary>
        [Test]
        public void UPDATE_ValidCondition()
        {
            Condition condition = new Condition("X", "x", 42);

            var serviceMock = new Mock<IConditionDataServices>();
            serviceMock.Setup(x => x.GetConditionById(condition.Id)).Returns(condition);
            serviceMock.Setup(x => x.UpdateCondition(condition)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(serviceMock.Object, loggerMock.Object);

            Assert.IsTrue(conditionServices.UpdateCondition(condition));
        }
    }
}
