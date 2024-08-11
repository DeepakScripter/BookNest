using BookNest.Models.Domain;
using BookNest.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BookNest.Repositories.Implementation
{
    // Implementation of the IGenreService interface
    public class GenreService : IGenreService
    {
        private readonly DatabaseContext context;

        // Constructor injection of DatabaseContext
        public GenreService(DatabaseContext context)
        {
            this.context = context;
        }

        // Method to add a genre
        public bool Add(Genre model)
        {
            try
            {
                context.Genre.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

        // Method to delete a genre by ID
        public bool Delete(int id)
        {
            try
            {
                var data = this.FindBy(id);
                if (data == null)
                    return false;
                context.Genre.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

        // Method to find a genre by ID
        public Genre FindBy(int id)
        {
            return context.Genre.Find(id);
        }

        // Method to get all genres
        public IEnumerable<Genre> GetAll()
        {
            return context.Genre.ToList();
        }

        // Method to update a genre
        public bool Update(Genre model)
        {
            try
            {
                context.Genre.Update(model);
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
