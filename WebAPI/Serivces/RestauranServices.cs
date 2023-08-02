using AutoMapper;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Exepction;
using WebAPI.Models;

namespace WebAPI.Serivces
{
    public class RestauranServices : IRestauranServices
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestauranServices> _logger;
        public RestauranServices(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestauranServices> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext.Restaurants
            .Include(r => r.Addres)
            .Include(r => r.Dishes)
            .FirstOrDefault(x => x.Id == id);
            if (restaurant == null)
            {
                throw new NotFoundExeption("restaurant  not found");
            }
            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var resutarants = _dbContext
            .Restaurants
            .Include(r => r.Addres)
            .Include(r => r.Dishes)
            .ToList();
            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(resutarants);
            return restaurantsDtos;

        }
        public int Create(CreateRestuarantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.Id;
        }
        public void Delete(int id)
        {
            //_logger.LogError(new Exception("error"), $"Restaruant with id: {id} Delete action invoked");
            var restaurant = _dbContext.Restaurants
            .FirstOrDefault(x => x.Id == id);

            if (restaurant == null) 
                throw new NotFoundExeption("restaurant not found");
            _dbContext.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        public void UpdateRestaurant(UpdateRestaurantDto dto)
        {
            var restaurant = _dbContext.Restaurants
            .FirstOrDefault(x => x.Id == dto.Id);

            if (restaurant == null)
            throw new NotFoundExeption("restaurant not found");

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;
            _dbContext.SaveChanges();
        }

    }
}
