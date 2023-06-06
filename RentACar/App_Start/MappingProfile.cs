using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using RentACar.Dto;
using RentACar.Models;

namespace RentACar.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //Mapper.CreateMap<Car, CarDTO>().ReverseMap(); Isto
            Mapper.CreateMap<Car, CarDTO>();
            Mapper.CreateMap<CarDTO, Car>();

            //Mapper.CreateMap<Customer, CustomerDTO>().ReverseMap(); Isto
            Mapper.CreateMap<Customer, CustomerDTO>();
            Mapper.CreateMap<CustomerDTO, Customer>();


            //Mapper.CreateMap<Rental, RentalDTO>().ReverseMap(); Isto
            Mapper.CreateMap<Rental, RentalDTO>();
            Mapper.CreateMap<RentalDTO, Rental>();
        }
    }
}