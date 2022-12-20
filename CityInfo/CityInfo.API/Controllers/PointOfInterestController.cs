using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointofinterest")]
    [ApiController]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;

        //constructor injection to inject ILoger
        public PointOfInterestController(ILogger<PointOfInterestController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
           
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterest>> GetPointsOfInterest(int cityId)
        {
            try
            {
            //    throw new Exception("sample");    

                var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

                if (city == null)
                {
                    _logger.LogInformation($"city with id {cityId} wasn't found when accessing points of interest");
                    return NotFound();
                }
                return Ok(city.pointOfInterests);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}", ex);
                return StatusCode(500, "A problem while handling your request"); 
            }
        }

        [HttpGet("{pointofinterestid}", Name ="GetPointOfInterest")]  ///getting one specifi point of interest
        public ActionResult<PointOfInterest> GetPointsOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }  

            //find point of interest
            var pointOfInterest = city.pointOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId); 
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        [HttpPost]
        public ActionResult<PointOfInterest> CreatePointOfInterest(int cityId, PointOfInterestForCreation pointOfInterest)
        {
          
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            //for demo purpose
            var maxPointOfInterestiD = CitiesDataStore.Current.Cities.SelectMany(u => u.pointOfInterests).Max(p => p.Id);
            var finalPointOfInterest = new PointOfInterest()
            {
                Id = cityId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            }; 

            city.pointOfInterests.Add(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = finalPointOfInterest.Id,
                },
                finalPointOfInterest);

        }
        [HttpPut("{pointofinterestid}")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
            PointOfInterestForUpdate pointOfInterest)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            //find point of interest
            var pointOfInterestFromStore= city.pointOfInterests
                .FirstOrDefault(c=>c.Id==pointOfInterestId); 
            if (pointOfInterestFromStore ==null)
            {
                return NotFound();
            }
            pointOfInterestFromStore.Name= pointOfInterest.Name;    
            pointOfInterestFromStore.Description= pointOfInterest.Description;
            return NoContent();
        }


        [HttpPatch("{pointofinterestid}")]
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
           JsonPatchDocument<PointOfInterestForUpdate> patchDocument)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.pointOfInterests
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            var pointOfInterestToPatch = new PointOfInterestForUpdate()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };
            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
            if (!ModelState.IsValid)
            {
              return BadRequest(ModelState);  
            }
            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }
                pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
                pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

                return NoContent();
            
        }

        [HttpDelete("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest(int cityId , int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            
            var pointOfInterestFromStore = city.pointOfInterests
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            city.pointOfInterests.Remove(pointOfInterestFromStore);
            return NoContent();
        }
    }
}
