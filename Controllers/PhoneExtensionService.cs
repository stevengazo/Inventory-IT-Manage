using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;

namespace InventoryIT.Controllers
{
    public class PhoneExtensionService: IControllerServices<PhoneExtension>  
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public PhoneExtensionService(InventoryDbContext inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public void Add(PhoneExtension entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(PhoneExtension entity)
        {
            throw new NotImplementedException();
        }

        public List<PhoneExtension> GetAll()
        {
            throw new NotImplementedException();
        }

        public PhoneExtension GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<PhoneExtension> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(PhoneExtension entity)
        {
            throw new NotImplementedException();
        }
    }
}
