using System.ComponentModel.DataAnnotations.Schema;
using Day2__Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace Day2__Lab.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        CompanyDbcontext db;
        public InstructorRepository(CompanyDbcontext dbcontext)
        {
            db = dbcontext;
        }
        public void Add(instructor entity)
        {
            db.instructor.Add(entity);
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

        public List<instructor> GetAll(string? include)
        {
            IQueryable<instructor> query = db.instructor.Where(i => i.IsDeleted != 1);
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

        public instructor GetById(int id)
        {
            return db.instructor.Where(i => i.IsDeleted != 1).FirstOrDefault(i => i.ID == id);
        }

        public List<instructor> GetWithPagintion(string? Include, int page, int pageSize)
        {
            return GetAll(Include).Where(i=>i.IsDeleted != 1)
                                  .OrderBy(i => i.ID)
                                  .Skip((page - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToList();
        }

        public int NumberOfInstructor()
        {
            return GetAll(string.Empty).Count();
        }

        public void save()
        {
            db.SaveChanges();
        }

        public void Update(instructor entity)
        {
            var instructor = GetById(entity.ID);
            instructor.name = entity.name;
            instructor.Address = entity.Address;
            instructor.Dept_id = entity.Dept_id;
            instructor.Image = entity.Image;
            instructor.salary = entity.salary;
            instructor.IsDeleted = entity.IsDeleted;
            instructor.Crs_id = entity.Crs_id;

            db.instructor.Update(instructor);
        }

    }
}
