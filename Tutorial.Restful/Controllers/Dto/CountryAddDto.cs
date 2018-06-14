using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tutorial.Restful.Controllers.Dto
{
    public class CountryAddDto
    {
        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string ChineseName { get; set; }
        public string Abbreviation { get; set; }
        public string Continent { get; set; }

        public List<CityDto> Cities { get; set; }
    }
}