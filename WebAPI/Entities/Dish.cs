namespace WebAPI.Entities
{
    public class Dish
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int? ResturantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
