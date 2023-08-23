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
    public class AbonamentServicesImplementation : IAbonamentServices
    {
        private IAbonamentDataServices abonamentDataServices;
        private IUtilizatorDataServices utilizatorDataServices;

        private ILog logger;

        public AbonamentServicesImplementation(IAbonamentDataServices abonamentDataServices, ILog logger, IUtilizatorDataServices utilizatorDataServices)
        {
            this.abonamentDataServices = abonamentDataServices;
            this.logger = logger;
            this.utilizatorDataServices = utilizatorDataServices;
        }

        public bool AddAbonament(Abonament abonament)
        {
            if (abonament != null)
            {
                var context = new ValidationContext(abonament, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(abonament, context, results, true))
                {
                    if (!this.abonamentDataServices.NameAlreadyExist(abonament.Name))
                    {
                        return this.abonamentDataServices.AddAbonament(abonament);
                    }
                    else
                    {
                        this.logger.Warn("Attempted to add an already existing abonament.");
                        return false;
                    }
                }
                else
                {
                    string message = null;
                    foreach (ValidationResult result in results)
                    {
                        Console.WriteLine(result.ErrorMessage);
                        message += result;
                    }
                    this.logger.Warn("Attempted to add an invalid abonament." + abonament.Name + " " + message);
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to add a null abonament.");
                return false;
            }
        }

        public bool AddAbonamentForUtilizator(string userEmail, string abonamentName)
        {
            if (userEmail != null)
            {
                if (abonamentName != null)
                {
                    if (this.utilizatorDataServices.EmailAlreadyExists(userEmail))
                    {
                        if (this.abonamentDataServices.FindAbonamentByName(abonamentName))
                        {
                            return this.abonamentDataServices.AddAbonamentForUtilizator(userEmail, abonamentName);
                        }
                        else
                        {
                            this.logger.Warn("Attempted to add inexisting abonament to the user.");
                            return false;
                        }
                    }
                    else
                    {
                        this.logger.Warn("Attempted to add an abonament to inexisting user.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to add a null abonament to an User");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to add an abonament to a null User Email.");
                return false;
            }
        }

        public MinutesSMSInternet CalculateAllNumberOfMinutesSMSInternet(List<AbonamentUser> abonaments, List<Bonusuri> bonusuri)
        {
            MinutesSMSInternet minutesSMSInternet = new MinutesSMSInternet(0, 0, 0, 0, 0, 0, 0, 0, 0);

            foreach (Abonament abonament in abonaments)
            {
                if(abonament != null)
                {
                    minutesSMSInternet.DurataConvorbireRetea += abonament.NumarMinuteRetea;
                    minutesSMSInternet.DurataConvorbireNationala += abonament.NumarMinuteNationale;
                    minutesSMSInternet.DurataConvorbireInternationala += abonament.NumarMinuteInternationale;
                    minutesSMSInternet.SMSRetea += abonament.SMSRetea;
                    minutesSMSInternet.SMSNationala += abonament.SMSNationale;
                    minutesSMSInternet.SMSInternationala += abonament.SMSInternationale;
                    minutesSMSInternet.TraficDeDateRetea += abonament.TraficDeDateRetea;
                    minutesSMSInternet.TraficDeDateNationala += abonament.TraficDeDateNationale;
                    minutesSMSInternet.TraficDeDateInternationala += abonament.TraficDeDateInternationale;
                }

            }

            foreach (Bonusuri bonus in bonusuri)
            {
                minutesSMSInternet.DurataConvorbireRetea += bonus.BonusConvorbireRetea;
                minutesSMSInternet.DurataConvorbireNationala += bonus.BonusConvorbireNationala;
                minutesSMSInternet.DurataConvorbireInternationala += bonus.BonusConvorbireInternationala;
                minutesSMSInternet.SMSRetea += bonus.BonusSMSRetea;
                minutesSMSInternet.SMSNationala += bonus.BonusSMSNationala;
                minutesSMSInternet.SMSInternationala += bonus.BonusSMSInternationala;
                minutesSMSInternet.TraficDeDateRetea += bonus.BonusTraficDeDateRetea;
                minutesSMSInternet.TraficDeDateNationala += bonus.BonusTraficDeDateNationala;
                minutesSMSInternet.TraficDeDateInternationala += bonus.BonusTraficDeDateInternationala;
            }

            return minutesSMSInternet;
        }

        public Abonament GetAbonamentById(int id)
        {
            return this.abonamentDataServices.GetAbonamentById(id);
        }

        public List<AbonamentUser> GetAbonamentsByUserId(int id)
        {
            return this.abonamentDataServices.GetAbonamentsByUserId(id);
        }
    }
}
