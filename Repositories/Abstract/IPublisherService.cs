using BookNest.Models.Domain;
namespace BookNest.Repositories.Abstract
{
    public interface IPublisherService
    {
        bool Add(Publisher model); // Method to add a publisher
        bool Update(Publisher model); // Method to update a publisher
        bool Delete(int id); // Method to delete a publisher by ID
        Publisher FindById(int id); // Method to find a publisher by ID
        IEnumerable<Publisher> GetAll(); // Method to get all publishers
    }
}
