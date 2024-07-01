using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Controllers
{
    public class FileService : IFilesService
    {
        private readonly InventoryDbContext dbContext;

        public FileService(InventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task Delete(FileModel file)
        {
            dbContext.File.Remove(file);
            dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<List<FileModel>> GetAllFilesAsync()
        {
            return dbContext.File.ToListAsync();
        }

        public Task<List<FileModel>> GetAllFilesComputerAsync(int id)
        {
            return dbContext.File.Where(i => i.ComputerModelID == id).ToListAsync();
        }

        public Task<List<FileModel>> GetAllFilesSmartPhoneAsync(int id)
        {
            return dbContext.File.Where(i => i.SmartPhoneModelId == id).ToListAsync();
        }

        public Task<List<FileModel>> GetAllPeripheralsAsync(int id)
        {
            return dbContext.File.Where(i => i.PeripheralModelId == id).ToListAsync();
        }

        public Task<List<FileModel>> GetAllPhoneAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FileModel> GetFileByIdAsync(int id)
        {
            return dbContext.File.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task SaveFileAsync(FileModel file)
        {
            dbContext.File.Add(file);
            await dbContext.SaveChangesAsync();
        }
    }
}
