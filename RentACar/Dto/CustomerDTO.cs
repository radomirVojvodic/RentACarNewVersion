using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Dto
{
    public class CustomerDTO
    {

        public int CustomerId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string DriverLicNo { get; set; }

    }
}