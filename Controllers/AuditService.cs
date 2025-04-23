using System.Linq.Expressions;
using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Controllers
{
    public class AuditService : IControllerServices<Audit>
    {
        private readonly InventoryDbContext _inventoryDb;

        public AuditService(InventoryDbContext inventoryDb)
        {
            this._inventoryDb = inventoryDb;
        }

        public void Add(Audit entity)
        {
            _inventoryDb.Audit.Add(entity);
            _inventoryDb.SaveChanges();
        }

        public void Delete(Audit entity)
        {
            _inventoryDb.Audit.Remove(entity);
            _inventoryDb.SaveChanges();
        }

        public List<Audit> GetAll()
        {
            _inventoryDb.Audit.OrderBy(i => i.AuditDate).ToList();
            return _inventoryDb.Audit.OrderBy(i => i.AuditDate).ToList();
        }

        public Audit GetById(int id)
        {
            return _inventoryDb.Audit.Find(id);
        }

        public List<Audit> Search(Expression<Func<Audit, bool>> predicate)
        {
            return _inventoryDb.Audit.Where(predicate).ToList();
        }

        public List<Audit> Search(string value)
        {
            return _inventoryDb.Audit
                .Where(i => i.AuditorName.Contains(value) || i.Comments.Contains(value))
                .ToList();
        }
        public void Update(Audit entity)
        {
            _inventoryDb.Audit.Update(entity);
            _inventoryDb.SaveChanges();
        }
    }
}
