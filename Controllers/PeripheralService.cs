using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;

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
            throw new NotImplementedException();
        }

        public void Delete(PeripheralModel entity)
        {
            throw new NotImplementedException();
        }

        public List<PeripheralModel> GetAll()
        {
            throw new NotImplementedException();
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
