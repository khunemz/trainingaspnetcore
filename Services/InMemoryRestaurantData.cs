﻿using System.Collections.Generic;
using System.Linq;
using OdeToFood.Entities;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private List<Restaurant> _restaurant;

        public InMemoryRestaurantData()
        {
            _restaurant = new List<Restaurant>()
            {
                new Restaurant() {Id = 1, Name = "The house of Kobe"},
                new Restaurant() {Id = 2, Name = "The house of Thai"},
                new Restaurant() {Id = 3, Name = "The house of Vietnam"},
                new Restaurant() {Id = 4, Name = "The house of Burmese"}

            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurant;
        }

        public Restaurant Get(int id)
        {
            return _restaurant.FirstOrDefault(r => r.Id== id);
        }
    }

    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
    }
}