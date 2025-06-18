using Day2__Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace Day2__Lab.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IcrsResultRepository crsResultRepository;
        private readonly IInstructorRepository instructorRepository;
        CompanyDbcontext db;
        public CourseRepository(IcrsResultRepository crsResultRepository,IInstructorRepository instructorRepository,CompanyDbcontext dbcontext)
        {
            db = dbcontext;
            this.crsResultRepository = crsResultRepository;
            this.instructorRepository = instructorRepository;
        }
        public void Add(Course entity)
        {
            db.Course.Add(entity);
        }

        public Course DataToValidate(string CourseName)
        {
            return GetAll("").Where(c => c.IsDeleted != 1).FirstOrDefault(c => c.Name == CourseName);
        }

        public bool Delete(int CourseId, int CourseDepartmentID)
        {
            var data = GitByCouseIDAndDepartmentID(CourseId, CourseDepartmentID);
            var condition = false;
            if (data != null)
            {
                data.IsDeleted = 1;
                Update(data);
                condition = true;
            }
            return condition;
        }

        public List<Course> GetAll(string? include)
        {
            IQueryable<Course> query = db.Course.Where(c => c.IsDeleted != 1);
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

        public Course GetById(int id)
        {
            return db.Course.Where(c => c.IsDeleted != 1).FirstOrDefault(c => c.ID == id);
        }

        public List<crsResult> GetCourcesWithPagination(string? Include, string CourseName, int page, int pageSize)
        {
            return crsResultRepository.GetAll(Include)
                               .Where(cr => cr.Course.Name == CourseName && cr.Trainee.IsDeleted != 1)
                               .OrderBy(cr => cr.Trainee.Name)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();
        }

        public List<instructor> GetInstructorCousre(string? Include, string CourseName)
        {
            return instructorRepository.GetAll(Include).Where(c => c.Course.Name == CourseName && c.IsDeleted != 1).ToList();
        }

        public Course GitByCouseIDAndDepartmentID(int CourseId, int CourseDepartmentID)
        {
           return db.Course.Where(c => c.IsDeleted != 1&&c.Department.IsDeleted!=1).FirstOrDefault(c => c.ID == CourseId && c.Dept_id == CourseDepartmentID);
        }

        public void save()
        {
            db.SaveChanges();
        }

        public int TotalNumberOfCourses(string? includes, string courseName)
        {
            return crsResultRepository.GetAll(includes).Where(cr => cr.Course.Name == courseName && cr.IsDeleted != 1).Count();
            
        }

        public int TotalPassedCourse(string? Include, string CourseName)
        {
            return crsResultRepository.GetAll(Include).Where(cr => cr.Course.Name == CourseName && cr.degree >= cr.Course.minDegree && cr.IsDeleted != 1).Count() ;
        }

        public void Update(Course entity)
        {
            var course = GetById(entity.ID);
            course.Name = entity.Name;
            course.degree = entity.degree;
            course.minDegree = entity.minDegree;
            course.Dept_id = entity.Dept_id;
            course.IsDeleted = entity.IsDeleted;    
            course.Hours = entity.Hours;

            db.Course.Update(course);
        }
    }
}
