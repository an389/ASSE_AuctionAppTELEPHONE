using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IUtilizatorServices
    {
        bool AddUtilizator(Utilizator utizator);
        List<Utilizator> GetAllUsers();
        Utilizator GetUtilizatorById(int id);
        Utilizator GetUtilizatorByEmailAndPassword(string email, string password);
        bool UpdateUtilizator(Utilizator utilizator);
        bool DeleteUtilizator(Utilizator utilizator);
        public string GetUtilizatorAbonamentsId(string email);

    }
}
