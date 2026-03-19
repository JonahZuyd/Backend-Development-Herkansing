using System;
using System.Collections.Generic;
using System.Text;
using ComponentHotelCL;

namespace ComponentHotel
{
    public class Program
    {
        static void Main(string[] args)
        {
            DAL dAL = new DAL();
            dAL.GetAllGuests();

        }
    }
}
