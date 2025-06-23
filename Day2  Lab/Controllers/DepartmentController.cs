using Day2__Lab.Repository;
using Day2__Lab.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Day2__Lab.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            return View("Index",departmentRepository.GetAll(string.Empty));
        }
        public IActionResult GitDepartmentReselt(int DeptId)
        {
            DepartmentWithInstructorsInfoVM deptData = new DepartmentWithInstructorsInfoVM();
            deptData.DeptId = DeptId;
            deptData.DepartmentName = departmentRepository.GetDepartmentName(DeptId);
            deptData.DepartmentManager = departmentRepository.GetDepartmentManager(DeptId);
            deptData.AllActiveCourses = departmentRepository.AllActiveCourses(DeptId);
            deptData.AllInstructorsSalary = departmentRepository.AllInstructorsSalary(DeptId);
            deptData.TotalInstructors = departmentRepository.TotalNumberOfInstructor(DeptId);
            return View("GitDepartmentReselt",deptData);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            return View("Edit",departmentRepository.GetById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDepartment(int id)
        {

            return View("Edit", departmentRepository.GetById(id));
        }
        public IActionResult Delete (int id)
        {
            bool condition =departmentRepository.Delete(id);
            if (condition)
            {
                departmentRepository.save();
                return Json(new {success=true});
            }
            return Json(new { success = false });
        }
    }
}
