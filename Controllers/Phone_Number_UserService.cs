using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Controllers
{
    public class Phone_Number_UserService : IControllerServices<Phone_Number_User_Model>
    {
        private readonly InventoryDbContext _dbContext;

        public Phone_Number_UserService(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Phone_Number_User_Model entity)
        {
            _dbContext.Phone_Number_User.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Phone_Number_User_Model entity)
        {
            throw new NotImplementedException();
        }

        public List<Phone_Number_User_Model> GetAll()
        {
            var D = _dbContext.Phone_Number_User
                .Include(i => i.PhoneNumber)
                .Include(i => i.Employee)
                .ThenInclude(e => e.Departament)
                .Include(i => i.PhoneNumberModel)
                .ThenInclude(PNM => PNM.Brand)
                .OrderBy(i => i.Employee.LastName)
                .ToList();
            return D;
        }

        public Phone_Number_User_Model GetById(int id)
        {
            return _dbContext.Phone_Number_User
                .Include(i => i.PhoneNumber)
                .Include(i => i.Employee)
                .ThenInclude(PNM => PNM.Departament)
                .Include(i => i.PhoneNumberModel)
                .ThenInclude(PNM => PNM.Brand)
                .FirstOrDefault(i => i.Id == id);
        }

        public List<Phone_Number_User_Model> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(Phone_Number_User_Model entity)
        {
            try
            {
                _dbContext.Phone_Number_User.Update(entity);
                _dbContext.SaveChanges();
            }
            catch (Exception r)
            {

                throw;
            }

        }
    }
}
