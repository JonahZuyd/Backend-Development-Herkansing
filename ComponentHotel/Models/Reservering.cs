using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentHotel.Models
{
    public class Reservering
    {
        [Key]
        public int ReserveringId { get; set; }
        
        public int GastId { get; set; }
        public Gast? Gast { get; set; }
        
        //meerdere kamers
        public ICollection<ReserveringHotelkamer> ReserveringHotelkamers { get; set; } = new List<ReserveringHotelkamer>();
        public DateTime IncheckDatum { get; set; }
        public DateTime UitcheckDatum { get; set; }
        public bool Status { get; set; }
    }
}
