﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Factura
    {

        [ExcludeFromCodeCoverage]
        public int Id { get; private set; }
        [Required(ErrorMessage = "[ClientEmail] cannot be null.")]
        [EmailAddress(ErrorMessage = "[ClientEmail] is not a valid email address.")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "[ClientEmail] must have between 5 and 50 digits.")]
        public string ClientEmail { get; set; }
        [Required(ErrorMessage = "[TVA] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[TVA] cannot be negative.")]
        public double TVA { get; set; }
        [Required(ErrorMessage = "[PretTotal] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[PretTotal] cannot be negative.")]
        public double PretTotal { get; set; }
        [Required(ErrorMessage = "[Achitat] cannot be null.")]
        public bool Achitat { get; set; }


        [ExcludeFromCodeCoverage]

        public Factura()
        {
        }

        public Factura(string clientEmail, double tVA, double pretTotal, bool achitat)
        {
            ClientEmail = clientEmail;
            TVA = tVA;
            PretTotal = pretTotal;
            Achitat = achitat;
        }
    }
}
