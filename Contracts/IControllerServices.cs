using System.Threading.Tasks;
using InventoryIT.Data;
using InventoryIT.Model;
namespace InventoryIT.Services
{
    public interface IControllerServices<T> where T : class
    {
        Task Add(T entity);

        Task Update(T entity);
        
        Task Delete(T entity);   

        Task<List<T>> GetAll();

        Task<List<T>> Search(string value);

    }
}


