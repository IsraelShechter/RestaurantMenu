
using RestaurantMenu.UI.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

namespace RestaurantMenu.UI.Repository
{
    public class ServingRepository : IServingRepository
    {
        private string pathToData;
        public ServingRepository(string pathToDataFile)
        {
            if (String.IsNullOrEmpty(pathToDataFile))
            {
                throw new ArgumentException("Path to data file is null or empty");
            }
            this.pathToData = pathToDataFile;
            if (!File.Exists(pathToDataFile))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(pathToDataFile));
                using (StreamWriter sw = File.CreateText(pathToDataFile))
                {
                    sw.WriteLine("[]");
                }
                    Seed();
                
            }
        }

        private List<Serving> GetJsonData()
        {
            List<Serving> servings;
            try
            {
                while (!IsFileReady(pathToData)) { }
                string json = File.ReadAllText(pathToData);
                servings = JsonConvert.DeserializeObject<List<Serving>>(json);
            }
            catch (IOException e)
            {
                throw new Exception("Problem with opening data file");
            }
            catch (Exception)
            {
                servings = null;
            }
            if (servings == null)
            {
                return Seed();
            }
            return servings;
        }

        private List<Serving> Seed()
        {
            List<Serving> sampleData = new List<Serving>();
            sampleData.Add(new Serving { Id = Guid.NewGuid(), Name = "ארוחת בוקר זוגית", Meal = Meal.Breakfast, Price = 35.48m });
            sampleData.Add(new Serving { Id = Guid.NewGuid(), Name = "שקשוקה", Meal = Meal.Breakfast, Price = 42m });
            sampleData.Add(new Serving { Id = Guid.NewGuid(), Name = "לחם שום", Meal = Meal.Breakfast, Price = 22.5m });
            sampleData.Add(new Serving { Id = Guid.NewGuid(), Name = "ארוחת צהרים זוגית", Meal = Meal.Lunch, Price = 71.90m });
            sampleData.Add(new Serving { Id = Guid.NewGuid(), Name = "מנה עסקית", Meal = Meal.Lunch, Price = 50m });
            sampleData.Add(new Serving { Id = Guid.NewGuid(), Name = "מרק", Meal = Meal.Lunch, Price = 35.2m });
            sampleData.Add(new Serving { Id = Guid.NewGuid(), Name = "שניצל", Meal = Meal.Lunch, Price = 40m });
            sampleData.Add(new Serving { Id = Guid.NewGuid(), Name = "ארוחת ערב זוגית", Meal = Meal.Dinner, Price = 82.3m });
            sampleData.Add(new Serving { Id = Guid.NewGuid(), Name = "ערב ישראלי", Meal = Meal.Dinner, Price = 41.6m });
            sampleData.Add(new Serving { Id = Guid.NewGuid(), Name = "חביתה", Meal = Meal.Dinner, Price = 15m });

            UpdateJsonData(sampleData);
            return sampleData;
        }

        private void UpdateJsonData(List<Serving> servings)
        {
            while (!IsFileReady(pathToData)) { }
            File.WriteAllText(pathToData, JsonConvert.SerializeObject(servings));
        }

        private bool IsFileReady(string filename)
        {
            try
            {
                using (FileStream inputStream = File.Open(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Write))
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Delete(Guid id)
        {
            List<Serving> servings = GetJsonData();
            int index = servings.FindIndex(s => s.Id == id);
            if (index != -1)
            {
                servings.RemoveAt(index);
                UpdateJsonData(servings);

            }
        }

        public Serving Get(Guid id)
        {
            List<Serving> servings = GetJsonData();
            return servings.Find(s => s.Id == id);

        }

        public IEnumerable<Serving> GetAll()
        {
            return GetJsonData();
        }

        public IEnumerable<Serving> GetByMeal(Meal meal)
        {
            List<Serving> servings = GetJsonData();
            return servings.FindAll(s => s.Meal == meal);
        }

        public void Insert(Serving entity)
        {
            List<Serving> servings = GetJsonData();
            servings.Add(entity);
            UpdateJsonData(servings);
        }

        public void Update(Serving entity)
        {
            List<Serving> servings = GetJsonData();
            int index = servings.FindIndex(s => s.Id == entity.Id);
            servings[index] = entity;
            UpdateJsonData(servings);
        }
    }
}
