using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;

namespace InventoryIT.Controllers
{
    public class DepartamentService : IControllerServices<Departament>
    {
        private readonly InventoryDbContext _db;

        public DepartamentService(InventoryDbContext db)
        {
            _db = db;
        }

        public void Add(Departament entity)
        {
            _db.Departament.Add(entity);    
            _db.SaveChanges();  
        }

        public void Delete(Departament entity)
        {
            throw new NotImplementedException();
        }

        public List<Departament> GetAll()
        {
            return _db.Departament.OrderBy(i=>i.Name).ToList();
        }

        public Departament GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Departament> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(Departament entity)
        {
            throw new NotImplementedException();
        }
    }
}
