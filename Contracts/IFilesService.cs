using InventoryIT.Model;
using Microsoft.AspNetCore.Components.Forms;

namespace InventoryIT.Contracts
{
    public interface IFilesService
    {
            Task SaveFileAsync(FileModel file);
            Task<FileModel> GetFileByIdAsync(int id);
            Task<IEnumerable<FileModel>> GetAllFilesAsync();    
    }
}
