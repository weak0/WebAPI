using WebAPI.Models;

namespace WebAPI.Serivces
{
    public interface IRestauranServices
    {
        int Create(CreateRestuarantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        bool Delete(int id);
        bool UpdateRestaurant(UpdateRestaurantDto dto);

    }
}