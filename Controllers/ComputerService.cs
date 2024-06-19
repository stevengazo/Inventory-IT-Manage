using InventoryIT.Data;
using InventoryIT.Model;
using InventoryIT.Contracts;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Controllers
{
    public class ComputerService : IControllerServices<ComputerModel>
    {
        private readonly InventoryDbContext _inventoryDb;

        public ComputerService(InventoryDbContext inventoryDb)
        {
            _inventoryDb = inventoryDb;
        }

        public void Add(ComputerModel entity)
        {
            _inventoryDb.Computer.Add(entity);
            _inventoryDb.SaveChanges();
        }

        public void Delete(ComputerModel entity)
        {
            throw new NotImplementedException();
        }

        public List<ComputerModel> GetAll()
        {
            return _inventoryDb.Computer.ToList();
        }

        public List<ComputerModel> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(ComputerModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
