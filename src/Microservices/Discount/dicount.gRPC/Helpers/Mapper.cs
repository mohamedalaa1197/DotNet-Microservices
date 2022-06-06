using AutoMapper;
using dicount.gRPC.Entities;
using discount.gRPC.Protos;

namespace dicount.gRPC.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}