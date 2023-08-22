using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    public class CentralaTelefonicaServicesImplementation : ICentralaTelefonicaService
    {
        private ILog logger;
        private ICentralaTelefonicaDataService centralaTelefonicaDataService;

        public CentralaTelefonicaServicesImplementation(ICentralaTelefonicaDataService centralaTelefonicaDataService, ILog logger)
        {
            this.logger = logger;
            this.centralaTelefonicaDataService = centralaTelefonicaDataService;
        }
        public bool AddCentralaTelefonica(CentralaTelefonica centralaTelefonica)
        {
            if (centralaTelefonica != null)
            {
                var context = new ValidationContext(centralaTelefonica, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(centralaTelefonica, context, results, true))
                {
                    return this.centralaTelefonicaDataService.AddCentralaTelefonica(centralaTelefonica);

                }
                else
                {
                    this.logger.Warn("Attempted to add an invalid centralaTelefonica.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to add a null centralaTelefonica.");
                return false;
            }
        }

        public List<CentralaTelefonica> GetUtilizatoriCentralaTelefonicaByEmail(int month, string email)
        {
            return this.centralaTelefonicaDataService.GetUtilizatorCentralaTelefonicaByEmail(month, email);
        }
    }
}
