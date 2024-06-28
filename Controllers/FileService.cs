using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;
using Microsoft.AspNetCore.Components.Forms;

namespace InventoryIT.Controllers
{
    public class FileService : IFilesService
    {
        private readonly InventoryDbContext dbContext;

        public FileService(InventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<IEnumerable<FileModel>> GetAllFilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FileModel> GetFileByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveFileAsync(FileModel file)
        {
            dbContext.File.Add(file);
            dbContext.SaveChangesAsync();
        }
    }
}
