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
    public class BonusuriServiceImplementation : IBonusuriService
    {
        public IBonusDataService bonusDataService;
        private ILog logger;

        public BonusuriServiceImplementation(IBonusDataService bonusDataService, ILog logger)
        {
            this.bonusDataService = bonusDataService;
            this.logger = logger;
        }

        public bool AddBonus(Bonusuri bonusuri)
        {

            if (bonusuri != null)
            {

                var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(bonusuri, context, results, true))
                {
                    if (!this.bonusDataService.NameAlreadyExist(bonusuri.Name))
                    {
                        return this.bonusDataService.AddBonus(bonusuri);
                    }
                    else
                    {
                        this.logger.Warn("Attempted to add an already existing bonusuri.");
                        return false;
                    }
                }
                else
                {
                    this.logger.Warn("Attempted to add an invalid bonusuri. ");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to add a null bonusuri.");
                return false;
            }
        }
        public Bonusuri GetBonusById(int id)
        {
            return this.bonusDataService.GetBonusById(id);
        }
    }
}
