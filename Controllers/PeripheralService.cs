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
            return _inventoryDb.Peripheral
                  .Include(i => i.Brand)
                  .Include(i=>i.Employee)
                  .ThenInclude(e=>e.Departament)
                  .FirstOrDefault(i => i.PeripheralModelId == id);
        }

        public List<PeripheralModel> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(PeripheralModel entity)
        {
            entity.Employee = entity.EmployeeId == 0 ? null : entity.Employee;
            entity.EmployeeId = entity.EmployeeId == 0 ? null : entity.EmployeeId;

            _inventoryDb.Peripheral.Update(entity);
            _inventoryDb.SaveChanges();
        }
    }
}
