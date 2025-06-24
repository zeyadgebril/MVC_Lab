using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Xml.Linq;
using Azure.Core;
using Day2__Lab.Models;
using Day2__Lab.Repository;
using Day2__Lab.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Day2__Lab.Controllers
{
    [Authorize(Roles = "admin,instructor")]
    public class CourseController : Controller
    {
        
        private readonly ICourseRepository courseRepository;
        private readonly IDepartmentRepository departmentRepository;

        public CourseController(ICourseRepository courseRepository,IDepartmentRepository departmentRepository)
        {
            this.courseRepository = courseRepository;
            this.departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            return View("Index", courseRepository.GetAll(""));
        }

        [HttpGet]
        public IActionResult GitCourseReselt(string crName, int page = 1)
        {
            if (crName != "-1")
            {
                int pageSize = 10;
                var totalCourseResults = courseRepository.TotalNumberOfCourses("Course,Trainee", crName);
                var CourseDatafromDB = courseRepository.GetCourcesWithPagination("Course,Trainee,Trainee.Department", crName, page, pageSize);
                var InstructorDataFromDB = courseRepository.GetInstructorCousre("Department,Course", crName);
                int pass = courseRepository.TotalPassedCourse("Course,Trainee", crName);
                var dataForValidation = courseRepository.DataToValidate(crName);


                CourseFinalResultVm crfVM = new CourseFinalResultVm();
                crfVM.ID = dataForValidation.ID;
                crfVM.CouseName = crName;
                crfVM.CouseDepartmentId = dataForValidation.Dept_id;
                crfVM.TotalStudent = totalCourseResults;
                crfVM.TotalPassed = pass;
                crfVM.TotalFaild = crfVM.TotalStudent - pass;
                crfVM.PassRate = crfVM.TotalStudent > 0 ? (pass * 100) / crfVM.TotalStudent : 0;


                #region Get Trainee list data
                List<CourseResultVM> temp = new List<CourseResultVM>();
                foreach (var data in CourseDatafromDB)
                {
                    CourseResultVM crVM = new CourseResultVM();
                    crVM.TranieeId = data.Traniee_id;
                    crVM.TranieeName = data.Trainee.Name;
                    crVM.Degree = data.degree;
                    crVM.TotalDegree = data.Course.degree;
                    crVM.DepartmentName = data.Trainee.Department.Name;

                    if (data.degree >= data.Course.minDegree)
                    {
                        crVM.Status = "Pass";
                        crVM.Color = "Green";
                    }
                    else
                    {
                        crVM.Status = "Fail";
                        crVM.Color = "Red";
                    }

                    var instructor = InstructorDataFromDB.Where(i => i.IsDeleted != 1).FirstOrDefault(i => i.Course.Name == data.Course.Name);
                    if (instructor != null)
                    {
                        crVM.InstructorName = instructor.name;
                    }

                    temp.Add(crVM);
                }
                #endregion

                crfVM.TraneeiList = temp;

                crfVM.CurrentPage = page;
                crfVM.PageSize = pageSize;
                crfVM.TotalItems = totalCourseResults;

                return View("GitCourseReselt", crfVM);
            }
            else
            {
                return View("NotFound");
            }
        }
        [Authorize(Roles ="admin")]
        public IActionResult AddNew()
        {
            CourseWithDeptList courseWithDeptList = new CourseWithDeptList();
            courseWithDeptList.DeptList=departmentRepository.GetAll("");
            courseWithDeptList.IsDeleted = 0;
            return View("AddNew",courseWithDeptList);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(CourseWithDeptList dataFromForm)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    if (dataFromForm.minDegree > dataFromForm.degree)
                    {
                        ModelState.AddModelError("minDegree", "Minimum degree must be less than or equal to total degree.");
                        dataFromForm.DeptList = departmentRepository.GetAll("");
                        return View("AddNew", dataFromForm);
                    }
                    Course course = new Course();
                    course.ID = dataFromForm.ID;
                    course.Name = dataFromForm.Name;
                    course.degree = dataFromForm.degree;
                    course.minDegree = dataFromForm.minDegree;
                    course.Dept_id = dataFromForm.Dept_id;
                    course.Hours = dataFromForm.Hours;
                    course.IsDeleted = dataFromForm.IsDeleted;  

                    courseRepository.Add(course);
                    courseRepository.save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Dept_id", "Select a department");
                }
            }
            dataFromForm.DeptList= departmentRepository.GetAll("");
            return View("AddNew", dataFromForm);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]

        public IActionResult Edit(int id)
        {
            var dataFromDb = courseRepository.GetById(id);
            CourseWithDeptList courseWithDeptList = new CourseWithDeptList();
            courseWithDeptList.ID = dataFromDb.ID;
            courseWithDeptList.Name = dataFromDb.Name;
            courseWithDeptList.Hours = dataFromDb.Hours;
            courseWithDeptList.degree = dataFromDb.degree;
            courseWithDeptList.minDegree = dataFromDb.minDegree;
            courseWithDeptList.DeptList = departmentRepository.GetAll("");
            return View("Edit", courseWithDeptList);

        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit(CourseWithDeptList dataFromForm)
        {
            if (ModelState.IsValid)
            {
                Course course = courseRepository.GetById(dataFromForm.ID);
                course.degree = dataFromForm.degree;
                course.minDegree = dataFromForm.minDegree;
                course.Dept_id = dataFromForm.Dept_id;
                course.Hours = dataFromForm.Hours;

                courseRepository.Update(course);
                courseRepository.save();
                return RedirectToAction("GitCourseReselt", new {crName=dataFromForm.Name,page = 1 });
            }
            CourseWithDeptList courseWithDeptList = new CourseWithDeptList();
            courseWithDeptList.DeptList = departmentRepository.GetAll("");
            return View("Edit", courseWithDeptList);

        }


        [HttpPost]
        [Authorize(Roles = "admin")]

        public IActionResult Delete(int id, int courseDepartmentID)
        {
            var condition = courseRepository.Delete(id, courseDepartmentID);
            if(condition)
            {
                courseRepository.save();
                return Json(new { success = true });
            }
            else
            {
                return View("NotFound");
            }
        }


        //Remote Validation 
        public IActionResult CheckMinDegree(int minDegree, int degree)
        {
          

            if (minDegree <= degree)
            {
                return Json(true);
            }
            return Json("Minimum degree must be less than or equal to total degree.");
        }
    }
}



