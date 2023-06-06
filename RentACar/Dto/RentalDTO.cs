using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Dto
{
    public class RentalDTO
    {

        public int RentalId { get; set; }

        [Required]
        //public CustomerDTO CustomerId { get; set; }
        public Nullable<int> CustomerId { get; set; }


        [Required]
        //public CarDTO CarId { get; set; }
        public Nullable<int> CarId { get; set; }


        [Required]
        public Nullable<System.DateTime> DateRented { get; set; }

        public Nullable<System.DateTime> DateReturned { get; set; }
    }
}