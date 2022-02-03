using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace Pepper.Models
{
    public partial class Promocje
    {
        public int Idpromocji { get; set; }
        public string Nazwa { get; set; }
        public string Link { get; set; }
        public string Opis { get; set; }
        public DateTime DataDodania { get; set; }

        public string Email { get; set; }
    }
}
