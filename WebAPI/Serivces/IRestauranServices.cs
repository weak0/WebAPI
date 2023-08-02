using WebAPI.Models;

namespace WebAPI.Serivces
{
    public interface IRestauranServices
    {
        int Create(CreateRestuarantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        void Delete(int id);
        void UpdateRestaurant(UpdateRestaurantDto dto);

    }
}