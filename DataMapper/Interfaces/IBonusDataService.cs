using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Interfaces
{
    public interface IBonusDataService
    {
        bool AddBonus(Bonusuri bonusuri);
        bool NameAlreadyExist(string name);
        public Bonusuri GetBonusById(int id);

    }
}
