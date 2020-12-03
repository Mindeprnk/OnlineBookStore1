using AutoMapper;
using OnlineBookStore1.Dtos;
using OnlineBookStore1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookStore1.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mappers for Domain to Dto objects
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Book, BookDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Genre, GenreDto>();

            //Mappers for Dto to Domain Objects
            Mapper.CreateMap<BookDto, Book>().
                ForMember(b => b.Id, opt=> opt.Ignore());
            Mapper.CreateMap<CustomerDto, Customer>().
                ForMember(c => c.Id,opt => opt.Ignore());
        }
    }
}