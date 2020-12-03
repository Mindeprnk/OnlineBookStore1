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
    public class BooksController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/books
        public IEnumerable<BookDto> Getbooks()
        {
            return _context.Book
                .Include(b => b.Genre)
                .ToList()
                .Select(Mapper.Map<Book,BookDto>);
        }

        //GET /api/books/1
        public IHttpActionResult GetBooks(int id)
        {
            var book = _context.Book.SingleOrDefault(b => b.Id == id);

            if (book == null)
               return NotFound();

            return Ok(Mapper.Map<Book, BookDto>(book));

        }

        //POST /api/books
        [HttpPost]
        public IHttpActionResult CreateBook(BookDto bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var book = Mapper.Map<BookDto, Book>(bookDto);

            _context.Book.Add(book);
            _context.SaveChanges();

            bookDto.Id = book.Id;

            return Created( new Uri(Request.RequestUri+"/"+book.Id),bookDto);
        }

        //PUT /api/books
        [HttpPut]
        public void UpdateBook(BookDto bookDto, int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var bookInDb = _context.Book.SingleOrDefault(b => b.Id == id);

            if (bookInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(bookDto, bookInDb);

            //bookInDb.Name = book.Name;
            //bookInDb.NumberInStock = book.NumberInStock;
            //bookInDb.ReleaseDate = book.ReleaseDate;
            //bookInDb.DateAdded = book.DateAdded;
            //bookInDb.GenreId = book.GenreId;
            //bookInDb.Genre = book.Genre;

            _context.SaveChanges();
            
        }

        //DELETE /api/books/1
        [HttpDelete]
        public void DeleteBook(int id)
        {
            var bookInDb = _context.Book.SingleOrDefault(b => b.Id == id);


            if (bookInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Book.Remove(bookInDb);
            _context.SaveChanges();
        }

    }
}
