using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentHotelCL
{
    public class Reservering
    {
        [Key]
        public int ReserveringId { get; set; }
        
        public int GastId { get; set; }
        public Gast? Gast { get; set; }
        
        //meerdere kamers
        public ICollection<ReserveringHotelkamer>? Hotelkamers { get; set; } = new List<ReserveringHotelkamer>();
        public DateTime IncheckDatum { get; set; }
        public DateTime UitcheckDatum { get; set; }
        public int TariefId { get; set; }
        public Tarief? Tarief { get; set; }
        public bool Status { get; set; }
    }
}
