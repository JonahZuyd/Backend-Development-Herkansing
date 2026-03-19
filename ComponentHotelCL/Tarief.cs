using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComponentHotelCL
{
    public class Tarief
    {
        [Key]
        public int TariefId { get; set; }

        // Relatie met reservering
        public int ReserveringId { get; set; }
        public Reservering? Reservering { get; set; }

        // Berekende velden
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotaal { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal KortingPercentage { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BedragNaKorting { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal ToeristenbelastingPercentage { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotaalBedrag { get; set; }

        public DateTime BerekendOp { get; set; } = DateTime.Now;
    }
}
