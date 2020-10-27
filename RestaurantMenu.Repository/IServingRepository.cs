using System;
using System.Collections.Generic;

namespace RestaurantMenu.UI.Repository
{
    public interface IServingRepository
    {
        IEnumerable<Serving> GetAll();
        IEnumerable<Serving> GetByMeal(Meal meal);
        Serving Get(Guid id);
        void Insert(Serving entity);
        void Update(Serving entity);
        void Delete(Guid id);
    }
}
