using InventoryIT.Model;

namespace InventoryIT.Contracts
{
    public interface IImageService
    {
        Task SaveFileAsync(ImageModel file);
        Task<ImageModel> GetFileByIdAsync(int id);
        Task<List<ImageModel>> GetAllFilesComputerAsync(int id);
        Task<List<ImageModel>> GetAllFilesSmartPhoneAsync(int id);
        Task<List<ImageModel>> GetAllPhoneAsync(int id);
        Task<List<ImageModel>> GetAllPeripheralsAsync(int id);
        Task Delete(ImageModel e);

    }
}
