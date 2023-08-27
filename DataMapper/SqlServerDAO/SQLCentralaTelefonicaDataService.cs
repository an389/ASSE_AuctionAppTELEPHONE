using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SqlServerDAO
{
    [ExcludeFromCodeCoverage]

    internal class SQLCentralaTelefonicaDataService : ICentralaTelefonicaDataService
    {
        private static readonly ILog Logger = LogManager.GetLogger(Environment.MachineName);

        public bool AddCentralaTelefonica(CentralaTelefonica centralaTelefonica)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.CentralaTelefonica.Add(centralaTelefonica);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding new centralaTelefonica: " + exception);
                    return false;
                }
            }
            Logger.Info("CentralaTelefonica added successfully!");
            return true;
        }

        public List<CentralaTelefonica> GetUtilizatorCentralaTelefonicaByEmail(int month, string email)
        {
            List<CentralaTelefonica> centralaTelefonica = null;
            using (var context = new AuctionContext())
            {
                centralaTelefonica = context.CentralaTelefonica.Where((centralaTelefonica) => centralaTelefonica.ClientEmail == email && 
                (centralaTelefonica.MomentulInceperiConvorbireRetea.Month == month ||
                centralaTelefonica.MomentulInceperiConvorbireInternationala.Month == month ||
                centralaTelefonica.SMSReteaDate.Month == month ||
                centralaTelefonica.SMSInternationalaDate.Month == month ||
                centralaTelefonica.SMSNationalaDate.Month == month ||
                centralaTelefonica.TraficDeDateReteaDate.Month == month ||
                centralaTelefonica.TraficDeDateNationalaDate.Month == month ||
                centralaTelefonica.TraficDeDateInternationalaDate.Month == month)).ToList();
            }

            return centralaTelefonica;
        }
    }
}
