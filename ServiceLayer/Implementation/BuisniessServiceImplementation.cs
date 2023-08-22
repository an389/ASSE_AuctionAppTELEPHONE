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
    public class BuisniessServiceImplementation : IBuisniessService
    {
     
        public IBuisniessDataService buisniessDataService;
        private ILog logger;

        public BuisniessServiceImplementation(IBuisniessDataService buisniessDataService, ILog logger)
        {
            this.buisniessDataService = buisniessDataService;
            this.logger = logger;
        }

        public bool AddBuisniess(Buisniess buisniess)
        {
            if (buisniess != null)
            {
                var context = new ValidationContext(buisniess, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(buisniess, context, results, true))
                {
                    return this.buisniessDataService.AddBuisniess(buisniess);
                }
                else
                {
                    this.logger.Warn("Attempted to add an invalid buisniess.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to add a null buisniess.");
                return false;
            }
        }

        public Buisniess GetBuisniess()
        {
            return this.buisniessDataService.GetBuisniess();
        }

        public Buisniess GetBuisniessById(int id)
        {
            return this.buisniessDataService.GetBuisniessById(id);
        }
    }
    
}
