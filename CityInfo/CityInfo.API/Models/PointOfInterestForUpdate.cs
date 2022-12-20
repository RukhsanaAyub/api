using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdate
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
