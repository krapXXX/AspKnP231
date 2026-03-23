

namespace AspKnP231.Data.Entities
{
    public class ShopProduct
    {
        public Guid Id { get; set; }

        public Guid? CategoryId { get; set; }

        public String Title { get; set; } = null!;

        public String Description { get; set; }

        public String Slug { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public double? Rating  { get; set; }
        public int Stock  { get; set; }

        public String ImageUrl { get; set; } = null!;
        public DateTime? DeletedAt { get; set; }
    }
}
