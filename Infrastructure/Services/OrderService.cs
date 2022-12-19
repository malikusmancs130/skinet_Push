using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IGenericRepository<Order> _orderRepo;
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IBasketRepository _basketRepo;
    private readonly IGenericRepository<DeliveryMethod> _dmRepo;
    public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<DeliveryMethod> dmRepo,
                        IGenericRepository<Product> productRepo, IBasketRepository basketRepo)
    {
        _basketRepo = basketRepo;
        _productRepo = productRepo;
        _dmRepo = dmRepo;
        _orderRepo = orderRepo;
    }

    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId,string basketId, Address shippingAddress)
    {
          // get a basket from the repo
        var basket = await _basketRepo.GetBasketAsync(basketId);

        // get items from product repo

        var items = new List<OrderItem>();
        foreach (var item in basket.items)
        {
            var productItem = await _productRepo.GetByIdAsync(item.Id);
            var itemOrdered = new ProductItemOrder(productItem.Id, productItem.Name, productItem.PictureUrl);
            var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
            items.Add(orderItem);
        }
        // get delivery method from repo

        var deliveryMethod = await _dmRepo.GetByIdAsync(deliveryMethodId);
        // cals subtotal
        var subtotal = items.Sum(item => item.Price * item.Quantity);
        // create total 

        var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
        // TODO: save to db 

        // return order
        return order;
    }

    public Task<IReadOnlyList<DeliveryMethod>> GetDeliverymethodsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
    {
        throw new NotImplementedException();
    }
}