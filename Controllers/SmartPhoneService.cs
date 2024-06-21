﻿using InventoryIT.Contracts;
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
            throw new NotImplementedException();
        }

        public List<SmartPhoneModel> GetAll()
        {
            return _inventoryDbContext.SmartPhone.Include(i=>i.Brand).ToList(); 
        }

        public SmartPhoneModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<SmartPhoneModel> Search(string value)
        {
            throw new NotImplementedException();
        }

        public void Update(SmartPhoneModel entity)
        {
            throw new NotImplementedException();
        }
    }
}