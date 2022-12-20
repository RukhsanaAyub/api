namespace CityInfo.API.Models
{
    public class City
    {

        public int Id { get; set; } 
        public string Name { get; set; }
        public string? Description { get; set; }

        public int NumberOfPointsOfInterest
        {
            get { 
                return pointOfInterests.Count; 
            }
        }

        public ICollection<PointOfInterest> pointOfInterests { get; set; }
        =new List<PointOfInterest>();    

    }
}
