﻿namespace ECommerce.Api.Products.Db
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; } = 0;
        public int Inventory { get; set; }
    }
}
