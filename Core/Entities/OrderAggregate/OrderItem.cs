namespace Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrder itemOrdered, decimal price, decimal quantity)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrder ItemOrdered { get; set; }

        public decimal Price { get; set; }
        public decimal Quantity { get; set; }


    }
}