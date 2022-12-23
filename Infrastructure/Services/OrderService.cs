using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBasketRepository _basketRepo;
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
        var result = await _unitOfWork.Complete();
        if (result <= 0) return null;

        // delete basket

        await _basketRepo.DeleteBasketAsync(basketId);

        // return order
        return order;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliverymethodsAsync()
    {
        return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
         var spec = new OrderWithItemsAndOrderingSpecification (id,buyerEmail);
         return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
    }

    public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
    {
       var spec = new OrderWithItemsAndOrderingSpecification (buyerEmail);
       
       return await _unitOfWork.Repository<Order>().ListAsync(spec);
    }
}