using AutoMapper;
using dicount.gRPC.Entities;

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