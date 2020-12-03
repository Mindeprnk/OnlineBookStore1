using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using OnlineBookStore1.Models;
using OnlineBookStore1.ViewModel;



namespace OnlineBooksStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        } 
        public ViewResult Index()
        {
            var books = _context.Book.Include(b => b.Genre).ToList();

            return View(books);
            //return View();
        }


      
      

        public ViewResult New()
        {
            var genretype = _context.Genres.ToList();
            

            var ViewModel = new BookFormViewModel()
            {
                Genres = genretype
            };

            return View("NewBook", ViewModel);
        }

        public ActionResult Edit(int id)
        {
            var book = _context.Book.SingleOrDefault(b => b.Id == id);

            if (book == null)
                return HttpNotFound();
            var viewModel = new BookFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View("NewBook", viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Book book)
        {
            if (!ModelState.IsValid)
            {
                var bookFormViewModel = new BookFormViewModel(book)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("NewBook",bookFormViewModel);
            }
            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _context.Book.Add(book);
            }
                
            else
            {
                var bookInDb = _context.Book.Single(b => b.Id == book.Id);

                bookInDb.Name = book.Name;
                bookInDb.GenreId = book.GenreId;
                bookInDb.NumberInStock = book.NumberInStock;
                bookInDb.ReleaseDate = book.ReleaseDate;
              
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Books");
        }
        public ActionResult Details(int id)
        {
            var book = _context.Book.Include(b => b.Genre).SingleOrDefault(b => b.Id == id);

            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

    }
}