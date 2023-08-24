using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class AbonamentUser : Abonament
    {
        [ExcludeFromCodeCoverage]
        public int Id { get; set; }
        public Utilizator Utilizator { get; set; }
        public AbonamentUser(Utilizator utilizator, Abonament abonament)
            : base(abonament.Name, abonament.Pret, abonament.StartDate, abonament.EndDate, abonament.NumarMinuteNationale, abonament.NumarMinuteInternationale, abonament.NumarMinuteRetea, abonament.SMSNationale, abonament.SMSInternationale, abonament.SMSRetea, abonament.TraficDeDateNationale, abonament.TraficDeDateInternationale, abonament.TraficDeDateRetea, abonament.BuissniesID)
        {
            this.Utilizator = utilizator;
        }
        [ExcludeFromCodeCoverage]
        public AbonamentUser()
        {

        }
    }
}
