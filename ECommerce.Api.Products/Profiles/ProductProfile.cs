using AutoMapper;
using ECommerce.Api.Products.Db;

namespace ECommerce.Api.Products.Profiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            this.CreateMap<Db.Product, Models.Product>();
        }
    }
}
