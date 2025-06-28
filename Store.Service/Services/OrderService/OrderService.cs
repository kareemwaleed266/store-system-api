using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.OrderSpecs;
using Store.Service.Services.BasketService;
using Store.Service.Services.OrderService.Dtos;
using Store.Service.Services.PaymentService;

namespace Store.Service.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork,
            IBasketService basketService,
            IMapper mapper,
            IPaymentService paymentService
            )
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _mapper = mapper;
            _paymentService = paymentService;
        }
        public async Task<OrderResultDto> CreateOrderAsync(OrderDto input)
        {
            var basket = await _basketService.GetBasketAsync(input.BasketId);

            if (basket is null)
                throw new Exception("Basket Not Exist");

            //get items of order
            var orderItems = new List<OrderItemsDto>();

            foreach (var basketItem in basket.BasketItems)
            {
                var productItem = await _unitOfWork.Repository<Product, int>().GetByIdAsync(basketItem.ProductId);
                if (productItem is null)
                    throw new Exception($"Product With Id {basketItem.ProductId} Not Exists");

                var itemOrdered = new ProductOrderedItem
                {
                    ProductItemId = productItem.Id,
                    ProductItemName = productItem.Name,
                    PictureUrl = productItem.PictureUrl,
                };

                var orderItem = new OrderItems
                {
                    Price = productItem.Price,
                    Quantity = basketItem.Quantity,
                    orderedItem = itemOrdered
                };

                var mappedOrderItem = _mapper.Map<OrderItemsDto>(orderItem);

                orderItems.Add(mappedOrderItem);
            }

            //Get delivery method
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId);

            if (deliveryMethod is null)
                throw new Exception("Delivery Method Is not Exist Right Now");

            var subTotal = orderItems.Sum(item => item.Quantity * item.Price);

            // to do => check if order exists
            var specs = new OrderWithPaymentIntentSpecification(basket.PaymentIntentId);

            var existingOrder = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);
            if (existingOrder != null)
            {
                _unitOfWork.Repository<Order, Guid>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(basket);
            }
            else
            {
                await _paymentService.CreateOrUpdatePaymentIntentForNewOrder(basket.Id);
            }

            // create order
            var mappedShippingAddress = _mapper.Map<ShippingAddress>(input.ShippingAddress);

            var mappedOrderItems = _mapper.Map<List<OrderItems>>(orderItems);

            var order = new Order
            {
                DeliveryMethodId = deliveryMethod.Id,
                ShippingAddress = mappedShippingAddress,
                BuyerEmail = input.BuyerEmail,
                OrderItems = mappedOrderItems,
                SubTotal = subTotal,
                BasketId = basket.Id,
                PaymentIntentId = basket.PaymentIntentId
            };
            await _unitOfWork.Repository<Order, Guid>().AddAsync(order);

            await _unitOfWork.CompleteAsync();

            var mappedOrder = _mapper.Map<OrderResultDto>(order);

            return mappedOrder;
        }           

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
        => await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();

        public async Task<IReadOnlyList<OrderResultDto>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var specs = new OrderWithItemsSpecification(buyerEmail);

            var orders = await _unitOfWork.Repository<Order, Guid>().GetAllWithSpecificationAsync(specs);

            if (orders is { Count: <= 0 })
                throw new Exception("You Do Not Have Orders Right Now");
            
            var mappedOrders = _mapper.Map<List<OrderResultDto>>(orders);

            return mappedOrders;
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id, string buyerEmail)
        {
            var specs = new OrderWithItemsSpecification(id, buyerEmail);

            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);

           if(order is null)
                throw new Exception($"There is No order With Id {id}");

            var mappedOrder = _mapper.Map<OrderResultDto>(order);

            return mappedOrder;
        }
    }
}
