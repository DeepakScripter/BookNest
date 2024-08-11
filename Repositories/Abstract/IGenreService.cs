using BookNest.Models.Domain;
namespace BookNest.Repositories.Abstract
{
    public interface IGenreService
    {
        bool Add(Genre model);
        bool Update(Genre model);
        bool Delete(int id);
        Genre FindBy(int id);
        IEnumerable<Genre> GetAll();

    }
}
