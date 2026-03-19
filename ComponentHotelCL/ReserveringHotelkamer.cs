using System;
using System.Collections.Generic;
using System.Text;

namespace ComponentHotelCL
{
    public class ReserveringHotelkamer
    {
        public int ReserveringId { get; set; }
        public Reservering? Reservering { get; set; }

        public int HotelkamerId { get; set; }
        public Hotelkamer? Hotelkamer { get; set; }
    }
}
