using BookNest.Models.Domain;

namespace BookNest.Repositories.Abstract
{
    public interface IBookService
    {
        bool Add(Book model); // Method to add a book
        bool Update(Book model); // Method to update a book
        bool Delete(int id); // Method to delete a book by ID
        Book FindById(int id); // Method to find a book by ID
        IEnumerable<Book> GetAll(); // Method to get all books
    }
}
