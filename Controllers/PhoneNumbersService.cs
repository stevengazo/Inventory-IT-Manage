using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;

namespace InventoryIT.Controllers
{
    public class PhoneNumbersService: IControllerServices<PhoneNumber>  
    {
        private readonly InventoryDbContext _inventoryDb;

        public PhoneNumbersService(InventoryDbContext inventoryDb)
        {
            _inventoryDb = inventoryDb;
        }

        public void Add(PhoneNumber entity)
        {
            _inventoryDb.PhoneNumber.Add(entity);
            _inventoryDb.SaveChanges();
        }

        public void Delete(PhoneNumber entity)
        {
            throw new NotImplementedException();
        }

        public List<PhoneNumber> GetAll()
        {
            return _inventoryDb.PhoneNumber.OrderBy(i=>i.Number).ToList();
        }

        public PhoneNumber GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<PhoneNumber> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(PhoneNumber entity)
        {
            throw new NotImplementedException();
        }
    }
}
