using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Serivces;

namespace WebAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {

        private readonly IRestauranServices _restaurantService;

        public RestaurantController(IRestauranServices restauranServices)
        {
            _restaurantService = restauranServices;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var resutarants = _restaurantService.GetAll();
            return resutarants.Any() ? Ok(resutarants) : BadRequest();
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);
            return Ok(restaurant);
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestuarantDto dto)
        {
            int id = _restaurantService.Create(dto);
            return Created($"/api/restuarant/{id}", "ok pomyslnie dodoano");
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);
            return NoContent();

        }
        [HttpPut]
        public ActionResult<RestaurantDto> UpdateRestaurant([FromBody] UpdateRestaurantDto dto)
        {
            _restaurantService.UpdateRestaurant(dto);
            var updatedRestaruant = _restaurantService.GetById(dto.Id);
            return Ok(updatedRestaruant);
        }
    }
}
