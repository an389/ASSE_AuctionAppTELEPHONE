using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IBonusuriService
    {
        bool AddBonus(Bonusuri bonusuri);
        public Bonusuri GetBonusById(int id);

    }
}
