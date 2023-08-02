namespace WebAPI.Entities
{
    public class Restaurant
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }

        public string ContactNumber { get; set; }
        public int AddresId { get; set; }
        public virtual Addres Addres { get; set; }

        public virtual List<Dish> Dishes { get; set; }

    }
}
