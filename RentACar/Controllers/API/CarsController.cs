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
    public class CarsController : ApiController
    {
        //Pomocu APIja zelimo da izvrsimo CRUD operacije
        // _context nam omogucava da pristupimo BP
        private RentACarEntities _context;

        public CarsController()
        {
            _context = new RentACarEntities();
        }


        //GET /api/cars
        public IEnumerable<CarDTO> GetCars()
        {
            var cars = _context.Cars.ToList();
            return cars.Select(Mapper.Map<Car, CarDTO>);
        }

        //GET /api/cars/1
        public IHttpActionResult GetCar(int id)
        {
            var car = _context.Cars.SingleOrDefault(b => b.CarId == id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Car, CarDTO>(car));
        }

        //POST /api/cars
        [HttpPost]
        public IHttpActionResult CreateCar(CarDTO carDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var car = Mapper.Map<CarDTO, Car>(carDto);
            _context.Cars.Add(car);
            _context.SaveChanges();

            carDto.CarId = car.CarId;

            // pomocu Created cemo vratiti novo kreirani entite carDto i statusni kod 201 i Uri gde cemo pronaci novokreirani entite
            return Created(new Uri(Request.RequestUri + "/" + car.CarId), carDto);
        }

        // PUT /api/cars/1
        [HttpPut]
        public void UpdateCar(int id, CarDTO carDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var dbCar = _context.Cars.SingleOrDefault(b => b.CarId == id);
            if (dbCar == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            //Mapirace podatke, prebaciti vrednost iz carDTO u dbCar
            Mapper.Map(carDTO, dbCar);

            _context.SaveChanges();
        }

        // DELETE /api/cars/1
        [HttpDelete]
        public void DeleteCar(int id)
        {
            var dbCar = _context.Cars.SingleOrDefault(b => b.CarId == id);
            if (dbCar == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Cars.Remove(dbCar);
            _context.SaveChanges();
        }
    }
}
