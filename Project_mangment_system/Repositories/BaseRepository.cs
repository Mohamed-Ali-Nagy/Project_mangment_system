using Project_management_system.Models;

namespace Project_management_system.Repositories
{
    public class BaseRepository<T>:IBaseRepository<T> where T :BaseModel
    {
    }
}
