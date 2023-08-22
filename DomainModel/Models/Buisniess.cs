using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Buisniess
    {
        public int Id { get; private set; }
        [Required(ErrorMessage = "[TVA] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[TVA] cannot be negative.")]
        public double TVA { get; set; }
        [Required(ErrorMessage = "[XPercentForClosingSooner] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[XPercentForClosingSooner] cannot be negative.")]

        public double XPercentForClosingSooner { get; set; }
        [Required(ErrorMessage = "[ProcentRaportareMinute] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[ProcentRaportareMinute] cannot be negative.")]
        public double ProcentRaportareMinute { get; set; }
        [Required(ErrorMessage = "[CursValutarEUR] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[CursValutarEUR] cannot be negative.")]
        public double CursValutarEUR { get; set; }
        [Required(ErrorMessage = "[CursValutarUSD] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[CursValutarUSD] cannot be negative.")]
        public double CursValutarUSD { get; set; }
        [Required(ErrorMessage = "[ProcentDepasireValoriAbonament] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[ProcentDepasireValoriAbonament] cannot be negative.")]
        public double ProcentDepasireValoriAbonament { get; set; }//daca depasesti se taxeaza cu urmatorul procent din pretul abonamentului ce e folosit in plus
      
        public double ProcentRaportare { get; set; }
        public Buisniess(double TVA, double xPercentForClosingSooner, double procentRaportareMinute, double cursValutarEUR, double cursValutarUSD, double procentDepasireValoriAbonament, double procentRaportare)
        {
            this.TVA = TVA;
            XPercentForClosingSooner = xPercentForClosingSooner;
            ProcentRaportareMinute = procentRaportareMinute;
            CursValutarEUR = cursValutarEUR;
            CursValutarUSD = cursValutarUSD;
            ProcentDepasireValoriAbonament = procentDepasireValoriAbonament;
            ProcentRaportare = procentRaportare;
        }
        public Buisniess()
        {
        }
    }
}
