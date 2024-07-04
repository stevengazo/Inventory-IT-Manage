using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Controllers
{
    public class ImageService : IImageService
    {
        private readonly InventoryDbContext inventoryDb;

        public ImageService(InventoryDbContext inventoryDb)
        {
            this.inventoryDb = inventoryDb;
        }

        public Task Delete(ImageModel e)
        {
            throw new NotImplementedException();
        }

        public List<ImageModel> GetAllFilesComputer(int id)
        {
           
            return  inventoryDb.Image
                .Where(i => i.ComputerModelID == id)
                .ToList();
        }

        public List<ImageModel> GetAllFilesSmartPhoneAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<ImageModel> GetAllPeripheralsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<ImageModel> GetAllPhoneAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ImageModel> GetFileByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveFileAsync(ImageModel file)
        {
            inventoryDb.Image.Add(file);
            inventoryDb.SaveChanges();
                return Task.CompletedTask;
        }
    }
}
