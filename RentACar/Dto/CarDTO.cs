using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Dto
{
    public class CarDTO
    {

        public int CarId { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string LicensePlate { get; set; }
        public Nullable<int> Year { get; set; }

        [Required]
        public Nullable<bool> Available { get; set; }

    }
}