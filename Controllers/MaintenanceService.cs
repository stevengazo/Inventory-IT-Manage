using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;

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
            throw new NotImplementedException();
        }

        public void Delete(Maintenance entity)
        {
            throw new NotImplementedException();
        }

        public List<Maintenance> GetAll()
        {
            throw new NotImplementedException();
        }

        public Maintenance GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Maintenance> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(Maintenance entity)
        {
            throw new NotImplementedException();
        }
    }
}
