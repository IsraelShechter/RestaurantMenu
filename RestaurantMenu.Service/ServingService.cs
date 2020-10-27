using System;
using System.Collections.Generic;
using System.Text;
using RestaurantMenu.UI.Repository;

namespace RestaurantMenu.UI.Service
{
    public class ServingService : IServingService
    {
        private IServingRepository servingRepository;

        public ServingService(IServingRepository servingRepository)
        {
            this.servingRepository = servingRepository;
        }

        public void DeleteServing(Guid id)
        {
            servingRepository.Delete(id);
        }

        public Serving GetServing(Guid id)
        {
            return servingRepository.Get(id);
        }

        public IEnumerable<Serving> GetServings(Meal meal)
        {
            return servingRepository.GetByMeal(meal);
        }

        public void InsertServing(Serving serving)
        {
            servingRepository.Insert(serving);
        }

        public void UpdateServing(Serving serving)
        {
            servingRepository.Update(serving);
        }
    }
}
