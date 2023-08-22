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
            if(utizator !=null)
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
                        this.logger.Warn("Attempted to add an already existing utilizator." + utizator.Emali);
                        return false;
                    }
                }
                else
                {
                    string message = null;
                    foreach (ValidationResult result in results)
                    {
                        message += result;
                    }

                    this.logger.Warn("Attempted to add an invalid utilizator." + utizator.Emali + " " + message);
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
            throw new NotImplementedException();
        }
        public string GetUtilizatorAbonamentsId(string email)
        {
            return this.utilizatorDataService.GeUtilizatortAbonamentsId(email);
        }

        public List<Utilizator> GetAllUsers()
        {
            return this.utilizatorDataService.GetAllUtilizatori();
        }

        public Utilizator GetUtilizatorByEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Utilizator GetUtilizatorById(int id)
        {
            return this.utilizatorDataService.GetUtilizatorById(id);
        }

        public bool UpdateUtilizator(Utilizator utilizator)
        {
            throw new NotImplementedException();
        }

        public void UserCloseAbonamentSooner(string userEmail, string abonament)
        {
            if (userEmail != null && abonament != null)
            {
                if (this.utilizatorDataService.EmailAlreadyExists(userEmail))
                {
                    this.utilizatorDataService.CloseAbonamentForUser(userEmail, abonament);
                }
                else
                {
                    this.logger.Warn("Cant find the user!");
                    Console.WriteLine("We cant find the user!");
                }
            }
            else
            {
                this.logger.Warn("Attempted to add a null utilizator.");
                Console.WriteLine("Attempted to add a null utilizator or null abonamentId.");
            }
        }
    }
}
