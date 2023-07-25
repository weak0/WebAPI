namespace WebAPI.Models
{
    public class UpdateRestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDelivery { get; set; }

    }
}
