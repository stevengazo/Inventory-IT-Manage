using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Controllers
{
    public class SmartPhoneService : IControllerServices<SmartPhoneModel>
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SmartPhoneService(InventoryDbContext inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public void Add(SmartPhoneModel entity)
        {
            _inventoryDbContext.SmartPhone.Add(entity);
            _inventoryDbContext.SaveChanges();
        }

        public void Delete(SmartPhoneModel entity)
        {
            entity.isActive =false;
            _inventoryDbContext.SmartPhone.Update(entity);
            _inventoryDbContext.SaveChanges();
        }

        public List<SmartPhoneModel> GetAll()
        {
            return _inventoryDbContext.SmartPhone.Include(i=>i.Brand).ToList(); 
        }

        public SmartPhoneModel GetById(int id)
        {
            return _inventoryDbContext.SmartPhone
                .Include(i=>i.Brand)
                .FirstOrDefault(i => i.SmartPhoneModelId == id);
        }

        public List<SmartPhoneModel> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(SmartPhoneModel entity)
        {
            _inventoryDbContext.SmartPhone.Update(entity);
            _inventoryDbContext.SaveChanges();
        }
    }
}
