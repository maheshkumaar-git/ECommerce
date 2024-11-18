using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService) 
        { 
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();
            var customersResult = await customersService.GetCustomersAsync();

            foreach (var order in ordersResult.Orders)
            {
                foreach (var item in order.Items)
                {
                    item.ProductName = productsResult.IsSuccess ? productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name
                        : "Product Info Not Available";
                }
            }

           if(ordersResult.IsSuccess)
           {
                var result = new
                {
                    Customer = customersResult.IsSuccess ? customersResult.Customers.FirstOrDefault(p => p.Id == customerId)?.Name
                        : "Customer Info Not Available",
                    Orders = ordersResult.Orders,
                };
                return (true,result);
           }
           return (false,null);
        }
    }
}
