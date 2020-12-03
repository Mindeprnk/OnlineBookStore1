using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OnlineBookStore1.Models;


namespace OnlineBookStore1.ViewModel
{
    public class BookFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public List<Customer> Customers { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number In Stock")]
        [Required]
        [Range(1,15)]
        public byte? NumberInStock { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte? GenreId { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Book": "New Book";
            }
        }
        public BookFormViewModel()
        {
            Id = 0;
            ReleaseDate = DateTime.Now;
            NumberInStock = 0;
        }

        public BookFormViewModel(Book book)
        {
            Id = book.Id;
            Name = book.Name;
            ReleaseDate = book.ReleaseDate;
            NumberInStock = book.NumberInStock;
            GenreId = book.GenreId;
        }
    }
}