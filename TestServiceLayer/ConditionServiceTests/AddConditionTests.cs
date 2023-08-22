// <copyright file="AddConditionTests.cs" company="Transilvania University of Brasov">
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
    ///     Test class for <see cref="ConditionServicesImplementation.AddCondition(DomainModel.Models.Condition)"/> method.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class AddConditionTests
    {
        /// <summary>Null condition log message.</summary>
        private const string LogAddNullCondition = "Attempted to add a null condition.";

        /// <summary>Invalid condition log message.</summary>
        private const string LogAddInvalidCondition = "Attempted to add an invalid condition.";

        /// <summary>Existing condition log message.</summary>
        private const string LogAddExistingCondition = "Attempted to add an already existing condition.";

        /// <summary>
        ///     Test for adding a null condition.
        /// </summary>
        [Test]
        public void ADD_NullCondition()
        {
            Condition nullCondition = null;

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.AddCondition(nullCondition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNullCondition)));
        }

        /// <summary>
        ///     Test for adding an invalid condition (a condition with null name).
        /// </summary>
        [Test]
        public void ADD_InvalidCondition_Name_Null()
        {
            Condition condition = new Condition(null, "description", 10);

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.AddCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidCondition))));
        }

        /// <summary>
        ///     Test for adding an invalid condition (a condition with empty name).
        /// </summary>
        [Test]
        public void ADD_InvalidCondition_Name_Empty()
        {
            Condition condition = new Condition(string.Empty, "description", 10);

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.AddCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidCondition))));
        }

        /// <summary>
        ///     Test for adding an invalid condition (a condition with name too long).
        /// </summary>
        [Test]
        public void ADD_InvalidCondition_Name_TooLong()
        {
            Condition condition = new Condition(new string('x', 16), "description", 10);

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.AddCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidCondition))));
        }

        /// <summary>
        ///     Test for adding an invalid condition (a condition with null description).
        /// </summary>
        [Test]
        public void ADD_InvalidCondition_Description_Null()
        {
            Condition condition = new Condition("X", null, 10);

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.AddCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidCondition))));
        }

        /// <summary>
        ///     Test for adding an invalid condition (a condition with empty description).
        /// </summary>
        [Test]
        public void ADD_InvalidCondition_Description_Empty()
        {
            Condition condition = new Condition("X", string.Empty, 10);

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.AddCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidCondition))));
        }

        /// <summary>
        ///     Test for adding an invalid condition (a condition with description too long).
        /// </summary>
        [Test]
        public void ADD_InvalidCondition_Description_TooLong()
        {
            Condition condition = new Condition("X", new string('x', 101), 10);

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.AddCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidCondition))));
        }

        /// <summary>
        ///     Test for adding an already existing condition.
        /// </summary>
        [Test]
        public void ADD_ValidCondition_ExistingCondition()
        {
            Condition condition = new Condition("X", "x", 42);

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionByName(condition.Name)).Returns(condition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.AddCondition(condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddExistingCondition)));
        }

        /// <summary>
        ///     Test for adding a valid condition.
        /// </summary>
        [Test]
        public void ADD_ValidCondition()
        {
            Condition condition = new Condition("X", "x", 42);
            Condition nullCondition = null;

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionByName(condition.Name)).Returns(nullCondition);
            conditionServiceMock.Setup(x => x.AddCondition(condition)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(conditionServices.AddCondition(condition));
        }
    }
}
