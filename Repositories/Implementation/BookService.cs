using System;
using System.Collections.Generic;
using System.Linq;
using BookNest.Models.Domain;
using BookNest.Repositories.Abstract;


namespace BookNest.Repositories.Implementation
{
    // Implementation of the IBookService interface
    public class BookService : IBookService
    {
        private readonly DatabaseContext context;

        // Constructor injection of DatabaseContext
        public BookService(DatabaseContext context)
        {
            this.context = context;
        }

        // Method to add a book
        public bool Add(Book model)
        {
            try
            {
                context.Book.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

        // Method to delete a book by ID
        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;
                context.Book.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

        // Method to find a book by ID
        public Book FindById(int id)
        {
            return context.Book.Find(id);
        }

        // Method to get all books
        public IEnumerable<Book> GetAll()
        {
            var data = (from book in context.Book
                        join author in context.Author
                        on book.AuthorId equals author.Id
                        join publisher in context.Publisher on book.PubhlisherId equals publisher.Id
                        join genre in context.Genre on book.GenreId equals genre.Id
                        select new Book
                        {
                            Id = book.Id,
                            AuthorId = book.AuthorId,
                            GenreId = book.GenreId,
                            Isbn = book.Isbn,
                            PubhlisherId = book.PubhlisherId,
                            Title = book.Title,
                            TotalPages = book.TotalPages,
                            GenreName = genre.Name,
                            AuthorName = author.AuthorName,
                            PublisherName = publisher.PublisherName
                        }
                        ).ToList();
            return data;
        }

        // Method to update a book
        public bool Update(Book model)
        {
            try
            {
                context.Book.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }
    }
}
