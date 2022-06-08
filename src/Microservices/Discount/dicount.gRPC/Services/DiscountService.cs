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
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,
                    $"Discount with {request.ProductName} is not found"));
            }

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> CreateDiscount(createDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            var isCreated = await _discountRepository.CreateDiscount(coupon);
            return isCreated ? _mapper.Map<CouponModel>(coupon) : null;
        }

        public override async Task<CouponModel> UpdateDiscount(updateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            var isUpdated = await _discountRepository.UpdateDiscount(coupon);
            return isUpdated ? _mapper.Map<CouponModel>(coupon) : null;
        }

        public override async Task<deleteDiscountResponse> DeleteDiscount(deleteDiscountRequest request,
            ServerCallContext context)
        {
            var isDeleted = await _discountRepository.Deletediscount(request.ProductName);
            return isDeleted
                ? new deleteDiscountResponse() { Succeded = true }
                : new deleteDiscountResponse() { Succeded = false };
        }
    }
}