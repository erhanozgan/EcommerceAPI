namespace ECommerce.Core.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Amount { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }

}