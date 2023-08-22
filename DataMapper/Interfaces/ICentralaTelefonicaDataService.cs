using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Interfaces
{
    public interface ICentralaTelefonicaDataService
    {
        bool AddCentralaTelefonica(CentralaTelefonica centralaTelefonica);
        List<CentralaTelefonica> GetUtilizatorCentralaTelefonicaByEmail(int month, string email);
    }
}
