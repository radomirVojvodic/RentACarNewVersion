using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RentACar.Models;
using System.Web.Routing;
using System.Data.Entity;
using RentACar.Dto;
using AutoMapper;

namespace RentACar.Controllers.API
{
    public class RentalsController : ApiController
    {

        //Pomocu APIja zelimo da izvrsimo CRUD operacije
        // _context nam omogucava da pristupimo BP
        private RentACarEntities _context;

        public RentalsController()
        {
            _context = new RentACarEntities();
        }

        //GET /api/rentals
        public IEnumerable<RentalDTO> GetRentals()
        {
            var rental = _context.Rentals.ToList();
            return rental.Select(Mapper.Map<Rental, RentalDTO>);
        }

        //GET /api/rentals/1
        public IHttpActionResult GetRentals(int id)
        {
            var rental = _context.Rentals.SingleOrDefault(b => b.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Rental, RentalDTO>(rental));
        }

        //POST /api/rentals
        [HttpPost]
        public IHttpActionResult CreateRental(RentalDTO rentalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var rental = Mapper.Map<RentalDTO, Rental>(rentalDto);
            _context.Rentals.Add(rental);
            _context.SaveChanges();

            rentalDto.RentalId = rental.RentalId;

            // pomocu Created cemo vratiti novo kreirani entite rentalDto i statusni kod 201 i Uri gde cemo pronaci novokreirani entite
            return Created(new Uri(Request.RequestUri + "/" + rental.RentalId), rentalDto);
        }

        // PUT /api/rentals/1
        [HttpPut]
        public void UpdateRental(int id, RentalDTO rentalDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var dbRental = _context.Rentals.SingleOrDefault(b => b.RentalId == id);
            if (dbRental == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            dbRental.DateRented = rentalDto.DateRented;
            dbRental.DateReturned = rentalDto.DateReturned;
            Mapper.Map(rentalDto, dbRental);

            _context.SaveChanges();

        }

        // DELETE /api/rentals/1
        [HttpDelete]
        public void DeleteRental(int id)
        {
            var dbRental = _context.Rentals.SingleOrDefault(b => b.RentalId == id);
            if (dbRental == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Rentals.Remove(dbRental);
            _context.SaveChanges();
        }
    }
}
