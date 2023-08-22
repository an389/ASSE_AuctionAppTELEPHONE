using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IAbonamentServices
    {
        bool AddAbonament(Abonament abonament);
        Abonament GetAbonamentById(int id);
        MinutesSMSInternet CalculateAllNumberOfMinutesSMSInternet(List<Abonament> abonaments, List<Bonusuri> bonusuri);
        bool AddAbonamentForUtilizator(string userEmail, string abonamentName);
        List<AbonamentUser> GetAbonamentsByUserId(int id);
    }
}
