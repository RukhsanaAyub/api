using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<City> Cities { get; set; }

        public static CitiesDataStore Current { get; } = new CitiesDataStore();  //return instance 

        public CitiesDataStore()
        {
            Cities = new List<City>()
            {
                new City()
                {
                    Id = 1,
                    Name = "Gilgit",
                    Description = "The city of mountains!",

                    pointOfInterests = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Id= 1,
                            Name= "Baltit Fort",
                            Description= "Baltit Fort is a fort in the Hunza valley, near the town of Karimabad, in the Gilgit-Baltistan region of northern Pakistan" },

                        new PointOfInterest()
                        {
                             Id= 2,
                            Name= "Altit Fort",
                            Description= "Altit Fort is an ancient fort in the Altit town in the Hunza valley in Gilgit Baltistan, Pakistan" }
                        }
            },
                new City()
                {
                    Id = 2,
                    Name = "Karachi",
                    Description = "The city of Lights!",
                 pointOfInterests = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Id= 3,
                            Name= "Clifton Beach",
                            Description= "Clifton beach, famously known as the sea view is a hub of recreational activities" },

                        new PointOfInterest()
                        {
                             Id= 4,
                            Name= "Mazar-e-Quaid",
                            Description= "Mazar-e-Quaid, also known as the Jinnah Mausoleum is one of the most visited places of Karachi." }
                        }

            },
                new City()
                {
                    Id = 3,
                    Name = "Lahore",
                    Description = "The city of pollution!",
                    pointOfInterests = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Id= 5,
                            Name= "Minar E Pakistan",
                            Description= "Minar E Pakistan is a tower located in Lahore, Pakistan. The tower was built between 1960 and 1968" },

                        new PointOfInterest()
                        {
                             Id= 6,
                            Name= "Lahore Museum",
                            Description= "Lahore Museum is Pakistan's largest museum, as well as one of its most visited ones." }
                        }
                        }

            };

            }

    }
    }
