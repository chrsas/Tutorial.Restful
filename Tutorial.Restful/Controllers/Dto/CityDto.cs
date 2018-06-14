using System.ComponentModel.DataAnnotations;

namespace Tutorial.Restful.Controllers.Dto
{
    public class CityDto
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}