using System.Collections.Generic;

namespace Tutorial.Restful.Domain.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string ChineseName { get; set; }
        public string Abbreviation { get; set; }

        public string Continent { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}