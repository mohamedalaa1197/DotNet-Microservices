using AutoMapper;
using EventBus.Message.Events;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>()
                    .ReverseMap();
        }
    }

}
