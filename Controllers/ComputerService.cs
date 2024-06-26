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
            return _inventoryDb.Computer.Include(i => i.Employee).ToList();
        }
        public ComputerModel GetById(int id) 
        {
            return _inventoryDb.Computer
                .Include(i => i.Employee)
                                .ThenInclude(PNM => PNM.Departament)
                .Include(i=>i.Brand)
                .FirstOrDefault(i => i.ComputerModelID == id);
        }


        public List<ComputerModel> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(ComputerModel entity)
        {
            entity.Employee = entity.EmployeeId == 0 ? null : entity.Employee;
            entity.EmployeeId = entity.EmployeeId == 0 ? null : entity.EmployeeId;

            _inventoryDb.Computer.Update(entity);
            _inventoryDb.SaveChanges();
        }


    }
}
