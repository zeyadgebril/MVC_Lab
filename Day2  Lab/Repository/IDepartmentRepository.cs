using Day2__Lab.Models;

namespace Day2__Lab.Repository
{
    public interface IDepartmentRepository:IRepository<Department>
    {
        public bool Delete(int id);
        public Department FindByName(string name);
        public string GetDepartmentManager(int DepartmentID);
        public string GetDepartmentName(int DepartmentID);

        public int TotalNumberOfInstructor(int DepartmentID);
        public int AllActiveCourses(int DepartmentID);
        public float AllInstructorsSalary(int DepartmentID);
    }
}
