namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrder
    {
        public ProductItemOrder()
        {
        }
        public ProductItemOrder(int productItemId, string productName, string picturalUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PicturalUrl = picturalUrl;
        }

        public int ProductItemId { get; set; }
        
        public string ProductName { get; set; }
        
        public string PicturalUrl { get; set; }
        
        
    }
}