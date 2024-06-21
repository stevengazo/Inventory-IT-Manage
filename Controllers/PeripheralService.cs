using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Controllers
{
    public class PeripheralService : IControllerServices<PeripheralModel> 
    {
        private readonly InventoryDbContext _inventoryDb;

        public PeripheralService(InventoryDbContext inventoryDb)
        {
            _inventoryDb = inventoryDb;
        }

        public void Add(PeripheralModel entity)
        {
            _inventoryDb.Peripheral.Add(entity);
            _inventoryDb.SaveChanges();
        }

        public void Delete(PeripheralModel entity)
        {
            throw new NotImplementedException();
        }

        public List<PeripheralModel> GetAll()
        {
           return _inventoryDb.Peripheral
                .Include(i=>i.Brand)
                .ToList();
        }

        public PeripheralModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<PeripheralModel> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(PeripheralModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
