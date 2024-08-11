using BookNest.Models.Domain;

namespace BookNest.Repositories.Abstract
{
    public interface IAuthorService
    {
        bool Add(Author model);
        bool Update(Author model);
        bool Delete(int id);
        Author FindBy(int id);
        IEnumerable<Author> GetAll();
    }
}
