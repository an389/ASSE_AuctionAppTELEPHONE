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
    public class UtilizatorServicesImplementation : IUtilizatorServices
    {
        private ILog logger;
        private IUtilizatorDataServices utilizatorDataService;

        public UtilizatorServicesImplementation(IUtilizatorDataServices utilizatorDataService, ILog logger)
        {
            this.logger = logger;
            this.utilizatorDataService = utilizatorDataService;
        }

        public bool AddUtilizator(Utilizator utizator)
        {
            if (utizator != null)
            {
                var context = new ValidationContext(utizator, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(utizator, context, results, true))
                {
                    if (!this.utilizatorDataService.EmailAlreadyExists(utizator.Emali))
                    {
                        return this.utilizatorDataService.AddUtilizator(utizator);
                    }
                    else
                    {
                        this.logger.Warn("Attempted to add an already existing utilizator.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to add an invalid utilizator.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to add a null utilizator.");
                return false;
            }
        }

        public bool DeleteUtilizator(Utilizator utilizator)
        {
            if (utilizator != null)
            {
                if (this.utilizatorDataService.GetUtilizatorById(utilizator.Id) != null)
                {
                    return this.utilizatorDataService.DeleteUtilizator(utilizator);
                }
                else
                {
                    this.logger.Warn("Attempted to delete a nonexisting user.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to delete a null user.");
                return false;
            }
        }


        public List<Utilizator> GetAllUsers()
        {
            return this.utilizatorDataService.GetAllUtilizatori();
        }

        public Utilizator GetUtilizatorById(int id)
        {
            return this.utilizatorDataService.GetUtilizatorById(id);
        }

        public bool UserCloseAbonamentSooner(string userEmail, string abonament)
        {
            if (userEmail != null && abonament != null)
            {
                if (this.utilizatorDataService.EmailAlreadyExists(userEmail))
                {
                   return this.utilizatorDataService.CloseAbonamentForUser(userEmail, abonament);
                }
                else
                {
                    this.logger.Warn("Cant find the user!");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to update a null utilizator.");
                return false;
            }
        }
    }
}
