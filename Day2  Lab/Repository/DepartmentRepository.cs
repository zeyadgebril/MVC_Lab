using Day2__Lab.Filter;
using Day2__Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace Day2__Lab.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        CompanyDbcontext db;
        private readonly IInstructorRepository instructorRepository;
        private readonly ICourseRepository courseRepository;

        public DepartmentRepository(CompanyDbcontext dbcontext,IInstructorRepository instructorRepository,ICourseRepository courseRepository)
        {
            db = dbcontext;
            this.instructorRepository = instructorRepository;
            this.courseRepository = courseRepository;
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
            var Department = GetById(id);
            bool Condition = false;
            if (Department != null)
            {
                Department.IsDeleted = 1;
                Update(Department);
                Condition = true;
            }
            return Condition;

        }

        public Department FindByName(string name)
        {
            return GetAll(string.Empty).FirstOrDefault(d => d.Name == name);
        }

        public int TotalNumberOfInstructor(int DepartmentID)
        {
            return instructorRepository.GetAll("Department").Where(i => i.Department.ID == DepartmentID && i.IsDeleted != 1 &&i.Department.IsDeleted!=1).Count();
        }

        public int AllActiveCourses(int DepartmentID)
        {
            return courseRepository.GetAll("Department").Where(c=>c.Dept_id==DepartmentID&&c.IsDeleted!=1&&c.Department.IsDeleted!=1).Count();
        }

        public float AllInstructorsSalary(int DepartmentID)
        {
            var salarys = 0.0f;
            foreach(var item in instructorRepository.GetAll(string.Empty))
            {
                if(item.Dept_id==DepartmentID&&item.IsDeleted!=1)
                {
                    salarys += item.salary;
                }
            }
            return salarys;
        }

        public string GetDepartmentManager(int DepartmentID)
        {
            return GetById(DepartmentID).Manager;
        }

        [HandellError]
        public string GetDepartmentName(int DepartmentID)
        {
            return GetById(DepartmentID).Name;

        }
    }
}
