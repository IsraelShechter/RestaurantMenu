using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.UI.Models;
using Microsoft.AspNetCore.JsonPatch;
using RestaurantMenu.UI.Service;
using RestaurantMenu.UI.Repository;

namespace RestaurantMenu.UI.Controllers.Api
{
    [Route("api/serving")]
    public class ServingApiController : Controller
    {
        private readonly IServingService servingService;

        public ServingApiController(IServingService servingService)
        {
            this.servingService = servingService;
        }

        [HttpGet("{meal}")]
        public IEnumerable<ServingViewModel> MealMenu(Meal meal)
        {
            List<ServingViewModel> results = new List<ServingViewModel>();
            servingService.GetServings(meal).ToList().ForEach(s =>
            {
                results.Add(new ServingViewModel { Id = s.Id, Name = s.Name, Meal = s.Meal, Price = s.Price });
            });
            return results;
        }

        [HttpPost]
        public void Serving([FromBody] ServingViewModel serving)
        {
            servingService.InsertServing(new Serving
            {
                Id = Guid.NewGuid(),
                Meal = serving.Meal,
                Name = serving.Name,
                Price = serving.Price
            });
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id) => servingService.DeleteServing(id);

        [HttpPatch("{id}")]
        public void Serving(Guid id,
            [FromBody] ServingViewModel patch)
        {
            servingService.UpdateServing(new Serving
            {
                Id = id,
                Meal = patch.Meal,
                Name = patch.Name,
                Price = patch.Price
            });
        }
    }
}
