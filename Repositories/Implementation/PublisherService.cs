using BookNest.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using BookNest.Repositories.Abstract;

namespace BookNest.Repositories.Implementation
{
    // Implementation of the IPublisherService interface
    public class PublisherService : IPublisherService
    {
        private readonly DatabaseContext context;

        // Constructor injection of DatabaseContext
        public PublisherService(DatabaseContext context)
        {
            this.context = context;
        }

        // Method to add a publisher
        public bool Add(Publisher model)
        {
            try
            {
                context.Publisher.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

        // Method to delete a publisher by ID
        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;
                context.Publisher.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

        // Method to find a publisher by ID
        public Publisher FindById(int id)
        {
            return context.Publisher.Find(id);
        }

        // Method to get all publishers
        public IEnumerable<Publisher> GetAll()
        {
            return context.Publisher.ToList();
        }

        // Method to update a publisher
        public bool Update(Publisher model)
        {
            try
            {
                context.Publisher.Update(model);
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
