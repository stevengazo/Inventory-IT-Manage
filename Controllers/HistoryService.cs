using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;

namespace InventoryIT.Controllers
{
    public class HistoryService : IHistoryServices<HistoryModel>
    {
        private readonly InventoryDbContext _inventoryDB;

        public HistoryService(InventoryDbContext inventoryDB)
        {
            _inventoryDB = inventoryDB;
        }

        public void Add(HistoryModel entity)
        {
            _inventoryDB.History.Add(entity);
            _inventoryDB.SaveChanges();
        }

        public void Delete(HistoryModel entity)
        {
            throw new NotImplementedException();
        }

        public List<HistoryModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public HistoryModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<HistoryModel> HistoriesOfComputer(int id)
        {
            var data = (from i in _inventoryDB.History
                        where i.ComputerModelID == id
                        select i).ToList();
            return data;
        }

        public List<HistoryModel> HistoriesOfSmartphones(int id)
        {
            var data = (from i in _inventoryDB.History
                        where i.SmartPhoneModelId == id
                        select i).ToList();
            return data;
        }

        public List<HistoryModel> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(HistoryModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
