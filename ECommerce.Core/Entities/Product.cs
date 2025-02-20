using System.ComponentModel.DataAnnotations;
using ECommerce.Core.Entities;

public class Product
{
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Basket> Baskets { get; set; }
}