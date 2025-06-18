using Day2__Lab.Models;

namespace Day2__Lab.Repository
{
    public interface IInstructorRepository:IRepository<instructor>
    {
        public bool Delete(int id);
        public int NumberOfInstructor();
        public List<instructor> GetWithPagintion(string? Include ,int page, int pageSize);

    }
}
