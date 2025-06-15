using System.Globalization;
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
            return View("Index", db.Course.ToList());
        }

        [HttpGet]
        public IActionResult GitCourseReselt(string crName, int page = 1)
        {
            int pageSize = 10;
            var totalCourseResults = db.crsResult.Include(cr => cr.Course)
                                                .Include(cr => cr.Trainee)
                                                .Where(cr => cr.Course.Name == crName)
                                                .Count();

            var CourseDatafromDB = db.crsResult.Include(cr => cr.Course)
                                              .Include(cr => cr.Trainee)
                                              .Include(cr=>cr.Trainee.Department)
                                              .Where(cr => cr.Course.Name == crName)
                                              .OrderBy(cr => cr.Trainee.Name) 
                                              .Skip((page - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToList();

            var InstructorDataFromDB = db.instructor.Include(i => i.Department)
                                                    .Include(i => i.Course)
                                                    .Where(c => c.Course.Name == crName)
                                                    .ToList();

            int pass = db.crsResult.Include(cr => cr.Course)
                                  .Include(cr => cr.Trainee)
                                  .Where(cr => cr.Course.Name == crName && cr.degree >= cr.Course.minDegree)
                                  .Count();

            CourseFinalResultVm crfVM = new CourseFinalResultVm();
            crfVM.CouseName = crName;
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
                crVM.DepartmentName =data.Trainee.Department.Name;

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

                var instructor = InstructorDataFromDB.FirstOrDefault(i => i.Course.Name == data.Course.Name);
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
    }
}
