using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SqlServerDAO
{
    public class SQLFacturaDataService : IFacturaDataService
    {
        private static readonly ILog Logger = LogManager.GetLogger(Environment.MachineName);

        public bool AddFactura(Factura factura)
        {
            using (AuctionContext context = new AuctionContext())
            {
                try
                {
                    context.Factura.Add(factura);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding new factura: " + exception);
                    return false;
                }
            }
            Logger.Info("factura added successfully!");
            return true;
        }
    }
}
