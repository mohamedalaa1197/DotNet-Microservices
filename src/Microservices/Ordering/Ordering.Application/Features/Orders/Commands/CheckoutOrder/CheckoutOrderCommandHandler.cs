using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Presistence;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
