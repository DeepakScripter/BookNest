using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BookNest.Models.Domain
{
    public class Book
    {
        public int Id { get; set; } // Unique identifier for the book

        [Required] // Specifies that Title is required
        public string Title { get; set; } // Title of the book

        [Required] // Specifies that Isbn is required
        public string Isbn { get; set; } // ISBN of the book

        [Required] // Specifies that TotalPages is required
        public int TotalPages { get; set; } // Total number of pages in the book

        [Required] // Specifies that AuthorId is required
        public int AuthorId { get; set; } // Foreign key for author

        [Required] // Specifies that PubhlisherId is required
        public int PubhlisherId { get; set; } // Foreign key for publisher

        [Required] // Specifies that GenreId is required
        public int GenreId { get; set; } // Foreign key for genre

        [NotMapped]
        public string? AuthorName { get; set; } // Name of the author (not mapped to database)

        [NotMapped]
        public string? PublisherName { get; set; } // Name of the publisher (not mapped to database)

        [NotMapped]
        public string? GenreName { get; set; } // Name of the genre (not mapped to database)

        [NotMapped]
        public List<SelectListItem>? AuthorList { get; set; } // List of authors for dropdown selection (not mapped to database)

        [NotMapped]
        public List<SelectListItem>? PublisherList { get; set; } // List of publishers for dropdown selection (not mapped to database)

        [NotMapped]
        public List<SelectListItem>? GenreList { get; set; } // List of genres for dropdown selection (not mapped to database)
    }
}
