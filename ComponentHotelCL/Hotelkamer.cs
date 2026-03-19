using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentHotelCL
{
    public class Hotelkamer
    {
        [Key]
        public int HotelkamerId { get; set; }
        
        public string Kamernummer { get; set; }
        public string Type { get; set; }
        public decimal PrijsPerNacht { get; set; }
        public bool IsBeschikbaar { get; set; }
        public ICollection<ReserveringHotelkamer> ReserveringHotelkamers { get; set; } 
            = new HashSet<ReserveringHotelkamer>();

    }
}
