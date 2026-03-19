using System;
using System.Collections.Generic;
using System.Text;

namespace ComponentHotelCL
{
    public class CreateUserDto
    {
        public int GastId { get; set; }
        public string WachtwoordHash { get; set; }
        public string Rol { get; set; }
    }
}
