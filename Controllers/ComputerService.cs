using InventoryIT.Data;
using InventoryIT.Model;
using InventoryIT.Services;
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

        public Task Add(ComputerModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(ComputerModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<ComputerModel>> GetAll()
        {
            return _inventoryDb.Computer.ToListAsync();
        }

        public Task<List<ComputerModel>> Search(string value)
        {
            throw new NotImplementedException();
        }

        public Task Update(ComputerModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
