using AutoMapper;
using ECommerce.Api.Orders.Db;

namespace ECommerce.Api.Orders.Profiles
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            this.CreateMap<Db.Order, Models.Order>();
            this.CreateMap<Db.OrderItem, Models.OrderItem>();
        }
    }
}
