using Day2__Lab.Models;
using NuGet.Protocol.Core.Types;

namespace Day2__Lab.Repository
{
    public interface ICourseRepository: IRepository<Course>
    {
        public bool Delete(int CourseId, int CourseDepartmentID);
        public Course GitByCouseIDAndDepartmentID(int CourseId, int CourseDepartmentID);
        public int TotalNumberOfCourses(string? includes, string courseName);
        public List<crsResult> GetCourcesWithPagination(string? Include,string CourseName,int page, int pageSize);
        public List<instructor> GetInstructorCousre(string? Include,string CourseName);
        public int TotalPassedCourse(string? Include, string CourseName);
        public Course DataToValidate(string CourseName);


    }
}
