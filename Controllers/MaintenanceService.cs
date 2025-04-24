using System.Linq;
using System.Linq.Expressions;
using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Controllers
{
    public class MaintenanceService : IControllerServices<Maintenance>
    {
        private readonly InventoryDbContext _inventoryDb;

        public MaintenanceService(InventoryDbContext inventoryDb)
        {
            this._inventoryDb = inventoryDb;
        }

        public void Add(Maintenance entity)
        {
            _inventoryDb.Maintenance.Add(entity);
            _inventoryDb.SaveChanges();
        }

        public void Delete(Maintenance entity)
        {
_inventoryDb.Maintenance.Remove(entity);
            _inventoryDb.SaveChanges();
        }

        public List<Maintenance> GetAll()
        {
            return _inventoryDb.Maintenance.ToList();
        }

        public Maintenance GetById(int id)
        {
        return _inventoryDb.Maintenance
                
                .Include(e=>e.Computer)
                .Include(e => e.Peripheral)
                .Include(e => e.SmartPhone)
                .FirstOrDefault(e=>e.MaintenanceId == id);
        }

        public List<Maintenance> Search(string value)
        {
            throw new NotImplementedException();
        }

        public List<Maintenance> Search(Expression<Func<Maintenance, bool>> predicate)
        {
            return _inventoryDb.Maintenance.Where(predicate).ToList();
        }

        public void Update(Maintenance entity)
        {
          _inventoryDb.Maintenance.Update(entity);
            _inventoryDb.SaveChanges(); 
        }
    }
}
