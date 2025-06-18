using Day2__Lab.Models;

namespace Day2__Lab.Repository
{
    public interface ITraineeRepository:IRepository<Trainee>
    {
        public bool Delete(int id);
    }
}
