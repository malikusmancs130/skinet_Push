using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;
    public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _basketRepo = basketRepo;
    }

    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
    {
        // get a basket from the repo
        var basket = await _basketRepo.GetBasketAsync(basketId);

        // get items from product repo

        var items = new List<OrderItem>();
        foreach (var item in basket.items)
        {
            var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
            var itemOrdered = new ProductItemOrder(productItem.Id, productItem.Name, productItem.PictureUrl);
            var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
            items.Add(orderItem);
        }
        // get delivery method from repo

        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
        // cals subtotal
        var subtotal = items.Sum(item => item.Price * item.Quantity);
        // create total 

        var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);

        _unitOfWork.Repository<Order>().Add(order);
        // TODO: save to db 
        var results = await _unitOfWork.Complete();

        if(results <= 0) return null;

        // delete Basket
        await _basketRepo.DeleteBasketAsync(basketId);
        
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