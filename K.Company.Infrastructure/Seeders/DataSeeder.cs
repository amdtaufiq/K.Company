using K.Company.Core.DAOs;
using K.Company.Infrastructure.Data;

namespace K.Company.Infrastructure.Seeders
{
    public class DataSeeder
    {
        private readonly KCompDBContext _context;

        public DataSeeder(KCompDBContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (!_context.Products.Any())
            {
                SeedProducts();
            }

            if (!_context.Customers.Any())
            {
                SeedCustomers();
            }

            if (!_context.Stores.Any())
            {
                SeedStores();
            }
        }

        private void SeedProducts()
        {
            List<Product> products = new List<Product>
        {
            new Product {ProductCode = "PRD1", ProductName = "Product 1", Description = "Description 1", ProductType = "Type 1", Brand = "Brand 1", UnitOfMeasurement = "Unit 1", CostOfGoodsSold = 10.0m, UnitPrice = 20.0m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow,
            Inventory = new Inventory { Quantity = 50, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow } },
            new Product {ProductCode = "PRD2", ProductName = "Product 2", Description = "Description 2", ProductType = "Type 2", Brand = "Brand 2", UnitOfMeasurement = "Unit 2", CostOfGoodsSold = 15.0m, UnitPrice = 25.0m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow,
            Inventory = new Inventory { Quantity = 75, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow } },
        };

            _context.Products.AddRange(products);
            _context.SaveChanges();
        }

        private void SeedCustomers()
        {
            List<Customer> customers = new List<Customer>
        {
            new Customer { CustomerName = "Customer 1", Address = "Address 1", Phone = "012221", CustomerType = "Mr", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Customer { CustomerName = "Customer 2", Address = "Address 2", Phone = "012222", CustomerType = "Mrs", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Customer { CustomerName = "Customer 3", Address = "Address 3", Phone = "012223", CustomerType = "Company", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        };

            _context.Customers.AddRange(customers);
            _context.SaveChanges();
        }

        private void SeedStores()
        {
            List<Store> stores = new List<Store>
        {
            new Store { StoreCode = "STR1", Description = "Description 1", Address = "Address 1", Phone = "112211", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Store { StoreCode = "STR2", Description = "Description 2", Address = "Address 2", Phone = "112212", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        };

            _context.Stores.AddRange(stores);
            _context.SaveChanges();
        }
    }
}
