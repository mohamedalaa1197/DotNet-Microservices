using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Message.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcServices _discountGrpcServices;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IBasketRepository basketRepository,
            DiscountGrpcServices discountGrpcServices, IMapper mapper
            , IPublishEndpoint publishEndpoint)
        {
            _basketRepository = basketRepository;
            _discountGrpcServices = discountGrpcServices;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }


        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            try
            {
                var basket = await _basketRepository.GetBasket(userName);
                return Ok(basket ?? new ShoppingCart { UserName = userName });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket(ShoppingCart shoppingCart)
        {
            try
            {
                foreach (var item in shoppingCart.Items)
                {
                    var coupon = await _discountGrpcServices.GetDiscount(item.ProductName);
                    item.Price -= coupon.Amount;
                }
                return await _basketRepository.UpdateBasket(shoppingCart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            try
            {
                await _basketRepository.DeleteBasket(userName);
                return Ok($"{userName} basket was deletde");
            }
            catch (Exception)
            {
                return BadRequest("Some thing bad happened");
            }

        }

        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout(CheckoutBasket checkoutBasketPayload)
        {
            // Get the basket for this user, to create an order with the items in it.
            var basket = await _basketRepository.GetBasket(checkoutBasketPayload.UserName);
            if (basket is null) return BadRequest();

            // mapping the payload to the evnet payload
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(checkoutBasketPayload);
            await _publishEndpoint.Publish(eventMessage);

            // Remove the basket after sending the event to order MS
            await _basketRepository.DeleteBasket(UserName: checkoutBasketPayload.UserName);

            return Accepted();
        }
    }
}
