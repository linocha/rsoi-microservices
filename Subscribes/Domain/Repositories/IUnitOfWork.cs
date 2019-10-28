using System.Threading.Tasks;

//A common pattern to handle issue (save data) is the Unit of Work Pattern. 
//This pattern consists of a class that receives our AppDbContext
//instance as a dependency and exposes methods to start, complete or abort transactions.
namespace Subscribes.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}