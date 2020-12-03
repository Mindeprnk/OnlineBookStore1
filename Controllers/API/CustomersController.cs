using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using OnlineBookStore1.Dtos;
using OnlineBookStore1.Models;

namespace OnlineBookStore1.Controllers.API
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers
                 .ToList()
                 .Select(Mapper.Map<Customer, CustomerDto>);
        }

        //GET /api/customers/1
        public IHttpActionResult GetCustomers(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);

        }

        //PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, Customer customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerinDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerinDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerinDb);
            //customerinDb.Name = customerDto.Name;
            //customerinDb.DateOfBirth = customerDto.DateOfBirth;
            //customerinDb.IsSubscribedToNewsLetter = customerDto.IsSubscribedToNewsLetter;
            //customerinDb.MembershipTypeId = customerDto.MembershipTypeId;

            _context.SaveChanges();


        }

        //DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerinDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerinDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerinDb);
            _context.SaveChanges();
        }
    }
}
