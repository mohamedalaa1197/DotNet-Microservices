using AutoMapper;
using Basket.API.Entities;
using EventBus.Message.Events;


namespace Basket.API.Mapper
{
    public class EntitiesMapping : Profile
    {
        public EntitiesMapping()
        {
            CreateMap<BasketCheckoutEvent, CheckoutBasket>()
                                                .ReverseMap();
        }
    }
}
