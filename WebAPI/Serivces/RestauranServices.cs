using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Models;

namespace WebAPI.Serivces
{
    public class RestauranServices : IRestauranServices
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        public RestauranServices(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext.Restaurants
            .Include(r => r.Addres)
            .Include(r => r.Dishes)
            .FirstOrDefault(x => x.Id == id);
            if (restaurant == null)
            {
                return null;
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
        public bool Delete(int id)
        {
            var restaurant = _dbContext.Restaurants
            .FirstOrDefault(x => x.Id == id);

            if (restaurant == null) return false;
            _dbContext.Remove(restaurant);
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateRestaurant(UpdateRestaurantDto dto)
        {
            var restaurant = _dbContext.Restaurants
            .FirstOrDefault(x => x.Id == dto.Id);
            if (restaurant == null) return false;
            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;
            _dbContext.SaveChanges();
            return true;
        }

    }
}
