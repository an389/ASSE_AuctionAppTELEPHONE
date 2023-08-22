using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Interfaces
{
    public interface IUtilizatorDataServices
    {
        bool AddUtilizator(Utilizator utilizator);
        List<Utilizator> GetAllUtilizatori();
        Utilizator GetUtilizatorById(int id);
        Utilizator GetUserByEmailAndPassword(string email, string password);    
        bool EmailAlreadyExists(string email);
        bool UserNameAlreadyExists(string userName);
        bool UpdateUtilizator(Utilizator utilizator);
        bool DeleteUtilizator(Utilizator utilizator);
        string GeUtilizatortAbonamentsId(string email);
        void CloseAbonamentForUser(string userEmail, string abonament);
    }
}
