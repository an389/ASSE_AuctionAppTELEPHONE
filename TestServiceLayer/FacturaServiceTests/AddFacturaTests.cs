using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using Moq;
using NUnit.Framework;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServiceLayer.FacturaServiceTests
{
    internal class AddFacturaTests
    {
        [Test]
        public void ValidFacturaValidation()
        {
            Factura factura = new Factura("andrei@fakemail.com", 1.2, 1200, false);

            var facturaServiceMock = new Mock<IFacturaDataService>();
            var loggerMock = new Mock<ILog>();
            facturaServiceMock.Setup(x => x.AddFactura(factura)).Returns(true);
            var facturaServiceMockImplementation = new FacturaServicesImplementation(facturaServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(facturaServiceMockImplementation.AddFactura(factura));
        }

        [Test]
        public void InvalidUser_Email_Null()
        {
            Factura factura = new Factura(null, 1.2, 120, false);
            var facturaServiceMock = new Mock<IFacturaDataService>();
            var loggerMock = new Mock<ILog>();
            facturaServiceMock.Setup(x => x.AddFactura(factura)).Returns(true);
            var facturaServiceMockImplementation = new FacturaServicesImplementation(facturaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(facturaServiceMockImplementation.AddFactura(factura));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid factura.")));
        }

        /// <summary>Test for invalid user (user with empty email address).</summary>
        [Test]
        public void InvalidUser_Email_Empty()
        {
            Factura factura = new Factura(string.Empty, 1.2, 120, false);
            var facturaServiceMock = new Mock<IFacturaDataService>();
            var loggerMock = new Mock<ILog>();
            facturaServiceMock.Setup(x => x.AddFactura(factura)).Returns(true);
            var facturaServiceMockImplementation = new FacturaServicesImplementation(facturaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(facturaServiceMockImplementation.AddFactura(factura));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid factura.")));
        }

        /// <summary>Test for invalid user (user with email address too long).</summary>
        [Test]
        public void InvalidUser_Email_TooLong()
        {
            Factura factura = new Factura(new string('x', 30) + '@' + new string('x', 30), 1.2, 120, false);
            var facturaServiceMock = new Mock<IFacturaDataService>();
            var loggerMock = new Mock<ILog>();
            facturaServiceMock.Setup(x => x.AddFactura(factura)).Returns(true);
            var facturaServiceMockImplementation = new FacturaServicesImplementation(facturaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(facturaServiceMockImplementation.AddFactura(factura));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid factura.")));
        }

        /// <summary>Test for invalid user (user with invalid email address).</summary>
        [Test]
        public void InvalidUser_Email_Invalid()
        {
            Factura factura = new Factura("andreifakemail.com", 1.2, 120, false);
            var facturaServiceMock = new Mock<IFacturaDataService>();
            var loggerMock = new Mock<ILog>();
            facturaServiceMock.Setup(x => x.AddFactura(factura)).Returns(true);
            var facturaServiceMockImplementation = new FacturaServicesImplementation(facturaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(facturaServiceMockImplementation.AddFactura(factura));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid factura.")));
        }

        [Test]
        public void InvalidNullFactura()
        {
            Factura factura = null;
            var facturaServiceMock = new Mock<IFacturaDataService>();
            var loggerMock = new Mock<ILog>();
            facturaServiceMock.Setup(x => x.AddFactura(factura)).Returns(true);
            var facturaServiceMockImplementation = new FacturaServicesImplementation(facturaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(facturaServiceMockImplementation.AddFactura(factura));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add a null factura.")));
        }
    }
}
