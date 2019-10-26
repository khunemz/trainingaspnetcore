using System.Collections.Generic;
using System.Linq;
using OdeToFood.Entities;

namespace OdeToFood.Services
{

    public class SqlRestaurantData : IRestaurantData {
        private OdeToFoodDbContext _context;

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants;
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
             
            _context.Add(newRestaurant);
            return newRestaurant;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        private static List<Restaurant> _restaurant;

        public InMemoryRestaurantData()
        {
            _restaurant = new List<Restaurant>
            {
                new Restaurant {Id = 1, Name = "The house of Kobe"},
                new Restaurant {Id = 2, Name = "The house of Thai"},
                new Restaurant {Id = 3, Name = "The house of Vietnam"},
                new Restaurant {Id = 4, Name = "The house of Burmese"}
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurant;
        }

        public Restaurant Get(int id)
        {
            return _restaurant.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = _restaurant.Max(r => r.Id) + 1;
            _restaurant.Add(newRestaurant);
            return newRestaurant;
        }

        public void Commit()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        Restaurant Add(Restaurant newRestaurant);
        void Commit();
    }
}