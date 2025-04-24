using System.Linq.Expressions;
using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;

namespace InventoryIT.Controllers
{
    public class AuditImageService : IControllerServices<AuditImage>
    {
        private readonly InventoryDbContext _inventoryDb;
        public AuditImageService(InventoryDbContext inventoryDbContext)
        {
            _inventoryDb = inventoryDbContext;
        }

        public void Add(AuditImage entity)
        {
            _inventoryDb.AuditImage.Add(entity);
            _inventoryDb.SaveChanges();
        }

        public void Delete(AuditImage entity)
        {
            _inventoryDb.AuditImage.Remove(entity);
            _inventoryDb.SaveChanges();
        }

        public List<AuditImage> GetAll()
        {
            _inventoryDb.AuditImage.ToList();
            return _inventoryDb.AuditImage.ToList();
        }

        public AuditImage GetById(int id)
        {
            return _inventoryDb.AuditImage.Find(id);
        }

        public List<AuditImage> Search(string value)
        {
            return  _inventoryDb.AuditImage
                .Where(x => x.AuditId.ToString().Contains(value) || x.AuditId.ToString().Contains(value))
                .ToList();
        }

        public List<AuditImage> Search(Expression<Func<AuditImage, bool>> predicate)
        {
            return _inventoryDb.AuditImage
                .Where(predicate)
                .ToList();
        }

        public void Update(AuditImage entity)
        {
            _inventoryDb.AuditImage.Update(entity);
            _inventoryDb.SaveChanges();
        }
    }
}
