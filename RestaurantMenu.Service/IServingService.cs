using RestaurantMenu.UI.Repository;
using System;
using System.Collections.Generic;

namespace RestaurantMenu.UI.Service
{
    public interface IServingService
    {
        IEnumerable<Serving> GetServings(Meal meal);
        Serving GetServing(Guid id);
        void InsertServing(Serving serving);
        void UpdateServing(Serving serving);
        void DeleteServing(Guid id);
    }
}
