using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Entities;
using System.Text.Json;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext Context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (Context.Brands != null && !Context.Brands.Any())
                {
                    var brandsData = File.ReadAllText("../Store.Repository/seedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    if (brands != null)
                    {
                        await Context.Brands.AddRangeAsync(brands);
                    }
                }

                if (Context.Types != null && !Context.Types.Any())
                {
                    var typesData = File.ReadAllText("../Store.Repository/seedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (types is not null)
                    {
                        await Context.Types.AddRangeAsync(types);
                    }
                }

                if (Context.Products != null && !Context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Store.Repository/seedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products != null)
                    {
                        await Context.Products.AddRangeAsync(products);
                    }
                }

                if (Context.DeliveryMethods != null && !Context.DeliveryMethods.Any())
                {
                    var deliveryMethodsData = File.ReadAllText("../Store.Repository/seedData/delivery.json");
                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                    if (deliveryMethods is not null)
                        await Context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                }

                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
