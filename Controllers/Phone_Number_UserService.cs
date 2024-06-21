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
            return _dbContext.Phone_Number_User
                .Include(i=>i.PhoneNumber)
                .Include(i => i.Employee)
                .Include(i => i.PhoneNumberModel)
                .ThenInclude(PNM=>PNM.Brand)
                .ToList();
        }

        public Phone_Number_User_Model GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Phone_Number_User_Model> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(Phone_Number_User_Model entity)
        {
            throw new NotImplementedException();
        }
    }
}
