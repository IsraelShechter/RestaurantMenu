using RestaurantMenu.UI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantMenu.UI.Models
{
    public class ServingViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Meal Meal { get; set; }
        public decimal Price { get; set; }
    }
}
