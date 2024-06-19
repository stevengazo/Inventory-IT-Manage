using InventoryIT.Services;
using InventoryIT.Model;
using InventoryIT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace InventoryIT.Controllers
{
    public class BrandService :  IControllerServices<Brand>
    {
        private readonly InventoryDbContext _inventoryDb;

        public BrandService(InventoryDbContext inventoryDb)
        {
            this._inventoryDb = inventoryDb;
        }

        public Task Add(Brand entity)
        {
            _inventoryDb.Brand.Add(entity);
            _inventoryDb.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public Task Delete(Brand entity)
        {
            _inventoryDb.Brand.Remove(entity);
            _inventoryDb.SaveChanges();
           return Task.CompletedTask;   
        }

        public async Task<List<Brand>> GetAll()
        {
            return await _inventoryDb.Brand.ToListAsync();
        }

        public Task<List<Brand>> Search(string value)
        {
            var data = (
                from i in _inventoryDb.Brand
                where i.Name == value
                select i).ToList(); 
            return Task.FromResult(data);
        }

        public Task Update(Brand entity)
        {
            _inventoryDb.Brand.Update(entity);
            _inventoryDb.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
