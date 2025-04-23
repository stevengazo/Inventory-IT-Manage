using System.Linq.Expressions;
using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;

namespace InventoryIT.Controllers
{
    public class MaintenanceImageService : IControllerServices<MaintenanceImage>
    {
        private readonly InventoryDbContext _inventoryDb;
        public MaintenanceImageService(InventoryDbContext inventoryDbContext)
        {
            _inventoryDb = inventoryDbContext;
        }

        public void Add(MaintenanceImage entity)
        {
           _inventoryDb.MaintenanceImage.Add(entity);
            _inventoryDb.SaveChanges();
        }

        public void Delete(MaintenanceImage entity)
        {
_inventoryDb.MaintenanceImage.Remove(entity);
            _inventoryDb.SaveChanges();
        }

        public List<MaintenanceImage> GetAll()
        {
_inventoryDb.MaintenanceImage.ToList();
            return _inventoryDb.MaintenanceImage.ToList();
        }

        public MaintenanceImage GetById(int id)
        {
            _inventoryDb.MaintenanceImage.Find(id);
            return _inventoryDb.MaintenanceImage.Find(id);
        }

        public List<MaintenanceImage> Search(string value)
        {
            throw new NotImplementedException();    
        }

        public List<MaintenanceImage> Search(Expression<Func<MaintenanceImage, bool>> predicate)
        {
            return _inventoryDb.MaintenanceImage.Where(predicate).ToList();
        }

        public void Update(MaintenanceImage entity)
        {
            _inventoryDb.MaintenanceImage.Update(entity);
            _inventoryDb.SaveChanges();
        }
    }
}
