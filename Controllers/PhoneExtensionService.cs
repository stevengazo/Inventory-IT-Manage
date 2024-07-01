using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Controllers
{
    public class PhoneExtensionService : IControllerServices<PhoneExtension>
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public PhoneExtensionService(InventoryDbContext inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public void Add(PhoneExtension entity)
        {
            _inventoryDbContext.PhoneExtension.Add(entity);
            _inventoryDbContext.SaveChanges();
        }

        public void Delete(PhoneExtension entity)
        {
            throw new NotImplementedException();
        }

        public List<PhoneExtension> GetAll()
        {
            return _inventoryDbContext.PhoneExtension.Include(i => i.Employee).ToList();
        }

        public PhoneExtension GetById(int id)
        {
            return _inventoryDbContext.PhoneExtension.Include(i => i.Employee).FirstOrDefault(i => i.PhoneExtensionId == id);
        }

        public List<PhoneExtension> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(PhoneExtension entity)
        {
            entity.EmployeeId = (entity.EmployeeId == 0) ? null : entity.EmployeeId;
            entity.Employee = (entity.EmployeeId == 0) ? null : entity.Employee;
            _inventoryDbContext.PhoneExtension.Update(entity);
            _inventoryDbContext.SaveChanges();
        }
    }
}
