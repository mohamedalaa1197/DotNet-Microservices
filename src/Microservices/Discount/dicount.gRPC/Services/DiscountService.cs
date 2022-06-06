using System.Threading.Tasks;
using AutoMapper;
using dicount.gRPC.Entities;
using dicount.gRPC.Repositories;
using discount.gRPC.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace dicount.gRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;


        public DiscountService(IMapper mapper, IDiscountRepository discountRepository, ILogger<DiscountService> logger)
        {
            _mapper = mapper;
            _discountRepository = discountRepository;
            _logger = logger;
        }

        public override async Task<CouponModel> GetDiscount(getDiscountRequest request, ServerCallContext context)
        {
            var coupon = _discountRepository.GetDiscount(request.ProductName);
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,
                    $"Discount with {request.ProductName} is not found"));
            }

            return _mapper.Map<CouponModel>(coupon);
        }
    }
}