using Day2__Lab.Models;

namespace Day2__Lab.Repository
{
    public interface IDepartmentRepository:IRepository<Department>
    {
        public bool Delete(int id);  
    }
}
