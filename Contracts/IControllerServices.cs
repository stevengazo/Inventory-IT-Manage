using System.Threading.Tasks;
using InventoryIT.Data;
using InventoryIT.Model;
namespace InventoryIT.Contracts
{
    public interface IControllerServices<T> where T : class
    {
        void Add(T entity);

        void Update(T entity);
        
        void Delete(T entity);   

        List<T> GetAll();

        List<T> Search(string value);

    }
}


