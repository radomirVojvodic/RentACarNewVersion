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
    public class CustomersController : ApiController
    {

        //Pomocu APIja zelimo da izvrsimo CRUD operacije
        // _context nam omogucava da pristupimo BP
        private RentACarEntities _context;

        public CustomersController()
        {
            _context = new RentACarEntities();
        }

        //GET /api/customers
        public IEnumerable<CustomerDTO> GetCustomers()
        {
            var customer = _context.Customers.ToList();
            return customer.Select(Mapper.Map<Customer, CustomerDTO>);
        }

        //GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(b => b.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Customer, CustomerDTO>(customer));
        }

        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = Mapper.Map<CustomerDTO, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.CustomerId = customer.CustomerId;

            // pomocu Created cemo vratiti novo kreirani entite customerDto i statusni kod 201 i Uri gde cemo pronaci novokreirani entite
            return Created(new Uri(Request.RequestUri + "/" + customer.CustomerId), customerDto);
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var dbCustomer = _context.Customers.SingleOrDefault(b => b.CustomerId == id);
            if (dbCustomer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            dbCustomer.Name = customerDto.Name;
            dbCustomer.DriverLicNo = customerDto.DriverLicNo;
            Mapper.Map(customerDto, dbCustomer);
            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var dbCustomer = _context.Customers.SingleOrDefault(b => b.CustomerId == id);
            if (dbCustomer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(dbCustomer);
            _context.SaveChanges();
        }
    }
}
