using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Models;
using WebAPI.Serivces;

namespace WebAPI.Controllers
{
    [Route("api/restaurant")]
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
            return resutarants.Any() ?  Ok(resutarants) :  BadRequest();
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);            
            return restaurant == null ? NotFound() : Ok(restaurant);
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestuarantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            int id = _restaurantService.Create(dto);
            return Created($"/api/restuarant/{id}", "ok pomyslnie dodoano");
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
          bool flag = _restaurantService.Delete(id);

            return flag ? NoContent() : NotFound();

        }
        [HttpPut]
        public ActionResult<RestaurantDto> UpdateRestaurant([FromBody] UpdateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool flag = _restaurantService.UpdateRestaurant(dto);
            var updatedRestaruant = _restaurantService.GetById(dto.Id);
            return flag ? Ok(updatedRestaruant) : BadRequest();
        }
    }
}
