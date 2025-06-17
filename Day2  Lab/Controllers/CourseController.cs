using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Xml.Linq;
using Azure.Core;
using Day2__Lab.Models;
using Day2__Lab.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Day2__Lab.Controllers
{
    public class CourseController : Controller
    {
        CompanyDbcontext db;
        
        public CourseController()
        {
            db = new CompanyDbcontext();
        }
        public IActionResult Index()
        {
            return View("Index", db.Course.Where(c => c.IsDeleted != 1).ToList());
        }

        [HttpGet]
        public IActionResult GitCourseReselt(string crName, int page = 1)
        {
           if(crName!="-1")
            {
                int pageSize = 10;
                var totalCourseResults = db.crsResult.Include(cr => cr.Course)
                                                    .Include(cr => cr.Trainee)
                                                    .Where(cr => cr.Course.Name == crName && cr.IsDeleted != 1)
                                                    .Count();

                var CourseDatafromDB = db.crsResult.Include(cr => cr.Course)
                                                  .Include(cr => cr.Trainee)
                                                  .Include(cr => cr.Trainee.Department)
                                                  .Where(cr => cr.Course.Name == crName && cr.Trainee.IsDeleted != 1)
                                                  .OrderBy(cr => cr.Trainee.Name)
                                                  .Skip((page - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .ToList();

               
                var InstructorDataFromDB = db.instructor.Include(i => i.Department)
                                                        .Include(i => i.Course)
                                                        .Where(c => c.Course.Name == crName && c.IsDeleted != 1)
                                                        .ToList();

                int pass = db.crsResult.Include(cr => cr.Course)
                                      .Include(cr => cr.Trainee)
                                      .Where(cr => cr.Course.Name == crName && cr.degree >= cr.Course.minDegree &&cr.IsDeleted != 1)
                                      .Count();
                var dataForValidation = db.Course.Where(c => c.IsDeleted != 1).FirstOrDefault(c => c.Name == crName);

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

                    var instructor = InstructorDataFromDB.Where(i=>i.IsDeleted!=1).FirstOrDefault(i => i.Course.Name == data.Course.Name);
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
        public IActionResult AddNew()
        {
            CourseWithDeptList courseWithDeptList = new CourseWithDeptList();
            courseWithDeptList.DeptList=db.Department.ToList();
            courseWithDeptList.IsDeleted = 0;
            return View("AddNew",courseWithDeptList);
        }

        [HttpPost]
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
                        dataFromForm.DeptList = db.Department.ToList();
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

                    db.Course.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Dept_id", "Select a department");
                }
            }
            dataFromForm.DeptList= db.Department.ToList();
            return View("AddNew", dataFromForm);
        }

        [HttpGet]

        public IActionResult Edit(int id)
        {
            var dataFromDb = db.Course.FirstOrDefault(c => c.ID == id);
            CourseWithDeptList courseWithDeptList = new CourseWithDeptList();
            courseWithDeptList.ID = dataFromDb.ID;
            courseWithDeptList.Name = dataFromDb.Name;
            courseWithDeptList.Hours = dataFromDb.Hours;
            courseWithDeptList.degree = dataFromDb.degree;
            courseWithDeptList.minDegree = dataFromDb.minDegree;
            courseWithDeptList.DeptList = db.Department.ToList();
            return View("Edit", courseWithDeptList);

        }

        public IActionResult SaveEdit(CourseWithDeptList dataFromForm)
        {
            if (ModelState.IsValid)
            {
                Course course = db.Course.FirstOrDefault(c => c.ID == dataFromForm.ID);
                course.degree = dataFromForm.degree;
                course.minDegree = dataFromForm.minDegree;
                course.Dept_id = dataFromForm.Dept_id;
                course.Hours = dataFromForm.Hours;

                db.Course.Update(course);
                db.SaveChanges();
                return RedirectToAction("GitCourseReselt", new {crName=dataFromForm.Name,page = 1 });
            }
            CourseWithDeptList courseWithDeptList = new CourseWithDeptList();
            courseWithDeptList.DeptList = db.Department.ToList();
            return View("Edit", courseWithDeptList);

        }


        [HttpPost]
        public IActionResult Delete(int id, int courseDepartmentID)
        {
            try
            {
                var CourseData = db.Course.FirstOrDefault(c => c.ID == id && c.Dept_id == courseDepartmentID);
                if (CourseData != null)
                {
                    CourseData.IsDeleted = 1;
                    db.Course.Update(CourseData);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return Json(new { success = true });
            }
            catch
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



