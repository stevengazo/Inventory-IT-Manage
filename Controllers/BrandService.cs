using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;

namespace InventoryIT.Controllers
{
    public class BrandService : IControllerServices<Brand>
    {
        private readonly InventoryDbContext _inventoryDb;

        public BrandService(InventoryDbContext inventoryDb)
        {
            this._inventoryDb = inventoryDb;
        }

        public void Add(Brand entity)
        {
            _inventoryDb.Brand.Add(entity);
            _inventoryDb.SaveChanges();

        }

        public void Delete(Brand entity)
        {
            _inventoryDb.Brand.Remove(entity);
            _inventoryDb.SaveChanges();
        }

        public List<Brand> GetAll()
        {
            return _inventoryDb.Brand.OrderBy(i => i.Name).ToList();
        }

        public Brand GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Brand> Search(string value)
        {
            var data = (
                from i in _inventoryDb.Brand
                where i.Name == value
                select i).ToList();
            return data;
        }

        public void Update(Brand entity)
        {
            _inventoryDb.Brand.Update(entity);
            _inventoryDb.SaveChanges();
        }
    }
}
