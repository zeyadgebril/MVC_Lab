using Day2__Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace Day2__Lab.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        CompanyDbcontext db;
        public DepartmentRepository(CompanyDbcontext dbcontext)
        {
            db = dbcontext;
        }
        public void Add(Department entity)
        {
            db.Department.Add(entity);
        }

        public List<Department> GetAll(string? include)
        {
            IQueryable<Department> query = db.Department.Where(d=>d.IsDeleted!=1);
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

        public Department GetById(int id)
        {
            return db.Department.Where(d => d.IsDeleted != 1).FirstOrDefault(d => d.ID == id);
        }

        public void save()
        {
            db.SaveChanges();
        }

        public void Update(Department entity)
        {
            var Department = GetById(entity.ID);
            Department.Name = entity.Name;
            Department.IsDeleted = entity.IsDeleted;
            Department.Manager = entity.Manager;

            db.Department.Update(Department);
        }

        public bool Delete(int id)
        {
            var Instructor = GetById(id);
            bool Condition = false;
            if (Instructor != null)
            {
                Instructor.IsDeleted = 1;
                Update(Instructor);
                Condition = true;
            }
            return Condition;

        }
    }
}
