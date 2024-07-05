using InventoryIT.Contracts;
using InventoryIT.Data;
using InventoryIT.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryIT.Controllers
{
    public class ImageService : IImageService
    {
        private readonly InventoryDbContext inventoryDb;
        private readonly IDbContextFactory<InventoryDbContext> _contextFactory;

        // El constructor acepta una fábrica de contextos (IDbContextFactory) para crear instancias de InventoryDbContext
        public ImageService(IDbContextFactory<InventoryDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
            // Se crea una instancia de InventoryDbContext usando la fábrica de contextos
            inventoryDb = _contextFactory.CreateDbContext();
        }

        public Task Delete(ImageModel e)
        {
            throw new NotImplementedException();
        }

        // Método para obtener todos los archivos relacionados con un modelo de computadora específico
        public Task<List<ImageModel>> GetAllFilesComputer(int id)
        {
            // Devuelve una lista de imágenes que tienen el mismo ComputerModelID que el ID proporcionado
            return inventoryDb.Image
                .Where(i => i.ComputerModelID == id)
                .ToListAsync();
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

        // Método para guardar un archivo de imagen en la base de datos
        public Task SaveFileAsync(ImageModel file)
        {
            // Agrega el archivo a la colección de imágenes
            inventoryDb.Image.Add(file);
            // Guarda los cambios en la base de datos
            inventoryDb.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
