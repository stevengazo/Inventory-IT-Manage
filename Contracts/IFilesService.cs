using InventoryIT.Model;
using Microsoft.AspNetCore.Components.Forms;

namespace InventoryIT.Contracts
{
    public interface IFilesService
    {
        Task SaveFileAsync(FileModel file);
        Task<FileModel> GetFileByIdAsync(int id);
        Task<List<FileModel>> GetAllFilesComputerAsync(int id);
        Task<List<FileModel>> GetAllPhoneAsync(int id);
        Task<List<FileModel>> GetAllPeripheralsAsync();
        Task Delete(FileModel e);

    }
}
