using InventoryIT.Services;
using InventoryIT.Model;
using InventoryIT.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Controllers
{
    public class EmployeeService : IControllerServices<Employee>
    {
        private readonly InventoryDbContext _InventoryDB;
        public EmployeeService(InventoryDbContext inventoryDbContext)
        {
            _InventoryDB = inventoryDbContext;  
        }

        public Task Add(Employee entity)
        {
            _InventoryDB.Employee.Add(entity);
            _InventoryDB.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public Task Delete(Employee entity)
        {
            _InventoryDB.Employee.Remove(entity);
            _InventoryDB.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _InventoryDB.Employee.ToListAsync();   
        }

        public Task<List<Employee>> Search(string value)
        {
            throw new NotImplementedException();
        }

        public Task Update(Employee entity)
        {
            _InventoryDB.Employee.Update(entity);
            _InventoryDB.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
