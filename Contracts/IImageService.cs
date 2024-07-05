using InventoryIT.Model;

namespace InventoryIT.Contracts
{
    public interface IImageService
    {
        Task SaveFileAsync(ImageModel file);
        Task<ImageModel> GetFileByIdAsync(int id);
        Task<List<ImageModel>> GetAllFilesComputer(int id);
        List<ImageModel> GetAllFilesSmartPhoneAsync(int id);
        List<ImageModel> GetAllPhoneAsync(int id);
       List<ImageModel> GetAllPeripheralsAsync(int id);
        Task Delete(ImageModel e);

    }
}
