using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentHotelCL
{
    public class Gebruiker
    {
        [Key]
        public int GebruikerId { get; set; }
        
        public int GastId { get; set; }
        public Gast? Gast { get; set; }
        public string Email { get; set; }
        public string WachtwoordHash { get; set; }
        public string Rol { get; set; }
    }
}
