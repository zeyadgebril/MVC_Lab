using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Day2__Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace Day2__Lab.Repository
{
    public class TraineeRepository : ITraineeRepository
    {
        CompanyDbcontext db;
        public TraineeRepository(CompanyDbcontext dbcontext)
        {
            db = dbcontext;
        }
        public void Add(Trainee entity)
        {
            db.Trainee.Add(entity);
        }

        public bool Delete(int id)
        {
            var Trainee = GetById(id);
            bool Condition = false;
            if (Trainee != null)
            {
                Trainee.IsDeleted = 1;
                Update(Trainee);
                Condition = true;
            }
            return Condition;

        }

        public List<Trainee> GetAll(string? include)
        {
            IQueryable<Trainee> query = db.Trainee.Where(t => t.IsDeleted != 1);
            if (!string.IsNullOrWhiteSpace(include))
            {
                var includes = include.Split(",");
                foreach (var item in includes)
                {
                    query = query.Include(item.Trim());
                }

            }
            return query.ToList();
        }

        public Trainee GetById(int id)
        {
            return db.Trainee.Where(t => t.IsDeleted != 1).FirstOrDefault(t => t.ID == id);
        }

        public void save()
        {
            db.SaveChanges();
        }

        public void Update(Trainee entity)
        {
            var Trainee = GetById(entity.ID);
            Trainee.Name = entity.Name;
            Trainee.Image = entity.Image;
            Trainee.Address = entity.Address;
            Trainee.grade = entity.grade;
            Trainee.Dept_id = entity.Dept_id;
            Trainee.IsDeleted = entity.IsDeleted;

            db.Trainee.Update(Trainee);
        }
    }
}
