using System.ComponentModel.DataAnnotations;

namespace ComponentHotelCL
{
    public class Gast
    {
        [Key]
        public int GastId { get; set; }

        public string Email { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Telefoonnummer { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Woonplaats { get; set; }
        public string Land { get; set; }
        public string IBAN { get; set; }
    }
}
