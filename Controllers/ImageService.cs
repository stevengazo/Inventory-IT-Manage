using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;

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

        public Task<List<ImageModel>> GetAllFilesComputerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ImageModel>> GetAllFilesSmartPhoneAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ImageModel>> GetAllPeripheralsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ImageModel>> GetAllPhoneAsync(int id)
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
