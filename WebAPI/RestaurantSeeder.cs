using WebAPI.Entities;

namespace WebAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();

                }
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {      
                new Role() 
                {
                    Name = "admin"
                },
                new Role()
                {
                    Name = "manager"
                },
                new Role()
                {
                    Name = "user"
                }

            };
            return roles;



        }
        public IEnumerable<Restaurant> GetRestaurants()
        {
            var result = new List<Restaurant>();
            Restaurant r1 = new Restaurant()
            {
                Name = "Dagrasso",
                Category = "Pizza",
                Description = "Friedns from pizza",
                ContactEmail = "test@test.pl",
                ContactNumber = "1",
                HasDelivery = true,
                Dishes = new List<Dish>()
                {
                new Dish()
                {
                    Name = "Pizza Salami",
                    Description = "aba",
                    Price = 10.3M,

                },
                new Dish()
                {
                    Name = "Pizza Margaritha",
                    Description ="baba",
                    Price = 9.5M,

                },
                },
                Addres = new Addres()
                {
                    City = "Katowice",
                    PostalCode = "40-175",
                    Street = "Kononowicza"

                }
            };
            Restaurant r2 = new Restaurant()
            {
                Name = "Bazyliana",
                Category = "Pizza",
                Description = "Pizza like in italy",
                ContactEmail = "kokoeurospoko@test.pl",
                ContactNumber = "2",
                HasDelivery = true,
                Dishes = new List<Dish>()
                {
                new Dish()
                {
                    Name = "Pizza Peproni",
                    Description ="juju",
                    Price = 12.3M,

                },
                new Dish()
                {
                    Name = "Pizza Casablanka",
                    Description = "jeje",
                    Price = 18.5M,

                },
                },
                Addres = new Addres()
                {
                    City = "Katowice",
                    PostalCode = "40-231",
                    Street = "Jana Pawła"

                }
            };
            result.Add(r2);
            result.Add(r1);
            return result;
        }
    }
}
