using AutoMapper;
using ECommerce.Api.Customers.Db;

namespace ECommerce.Api.Customers.Profiles
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            this.CreateMap<Db.Customer, Models.Customer>();
        }
    }
}
