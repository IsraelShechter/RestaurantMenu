using System;

namespace RestaurantMenu.UI.Repository
{
    public class Serving
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Meal Meal { get; set; }
        public decimal Price { get; set; }
    }
}
