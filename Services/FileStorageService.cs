using Microsoft.AspNetCore.Components.Forms;

namespace InventoryIT.Services
{
    public class FileStorageService
    {
        private readonly string _baseRoute = Path.Combine(Directory.GetCurrentDirectory(), "filesData");

        /// <summary>
        /// Create a new directory for the files uploaded
        /// </summary>
        /// <param name="Id">id of the folder</param>
        /// <returns></returns>
        public async Task GenerateDirectoryAsync(string folderName)
        {
            try
            {
                var folder = Path.Combine(_baseRoute, folderName);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Upload a file to the server
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> UploadFilesAsync(string folderName, IBrowserFile file)
        {
            // Generar directorios si no existen
            await GenerateDirectoryAsync(folderName);

            // Directorio de la factura
            var billDirectory = Path.Combine(_baseRoute, folderName);

            var fileName = Path.GetFileName(file.Name);
            var filePath = Path.Combine(billDirectory, fileName);

            // Guardar el archivo en el directorio correspondiente
            using (var stream = file.OpenReadStream(maxAllowedSize: 20 * 1024 * 1024))
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await stream.CopyToAsync(fileStream);
                }
            }

            // Agregar la ruta relativa del archivo
            var relativePath = Path.Combine("filesData", folderName, fileName);

            return relativePath;
        }

        /// <summary>
        /// Get the files in a directory
        /// </summary>
        /// <param name="folderName"> Folder to Search</param>
        /// <returns></returns>
        public async Task<List<string>> GetFilesAsync(string folderName)
        {
            var folder = Path.Combine(_baseRoute, folderName);
            if (!Directory.Exists(folder))
            {
                return new List<string>();
            }

            var files = Directory.GetFiles(folder);
            return files.Select(file => Path.Combine("filesData", folderName, Path.GetFileName(file))).ToList();
        }

        /// <summary>
        /// Delete a file from the server
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public async Task DeleteFolderAsync(string folderName)
        {
            var folder = Path.Combine(_baseRoute, folderName);
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }
            await Task.CompletedTask;
        }   
    }

}
