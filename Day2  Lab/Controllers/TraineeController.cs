using System.Runtime.Intrinsics.Arm;
using Day2__Lab.Models;
using Day2__Lab.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day2__Lab.Controllers
{
    public class TraineeController : Controller
    {
        CompanyDbcontext db;
        public TraineeController()
        {
            db = new CompanyDbcontext();
        }
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public IActionResult GetTraineeCourseResult()
        {
            return View("GetTraineeCourseResult");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SeeResult(TraineNameAndCourseVM tc)
        {
            var resFromDB = db.crsResult.Include(cr => cr.Trainee)
                                      .Include(cr => cr.Course)
                                      .FirstOrDefault(cr => cr.Trainee.Name == tc.TraineeName && cr.Course.Name == tc.CourseName);

            #region Cookies&sSession 


            //HttpContext.Session.SetString("Trainee Name session", resFromDB.Trainee.Name);
            //HttpContext.Response.Cookies.Append("CourseNamecookie",resFromDB.Course.Name);
            #endregion
            if (resFromDB == null)
            {
                return View("NotFoundTrainee");
            }
                TraineeResultVm trVM = new TraineeResultVm();

                trVM.tName = resFromDB.Trainee.Name;
                trVM.cName = resFromDB.Course.Name;
                trVM.Degree = resFromDB.degree;
                trVM.CourseDegree = resFromDB.Course.degree;
                trVM.Mindegree = resFromDB.Course.minDegree;
                if (trVM.Degree >= trVM.Mindegree)
                {
                    trVM.Status = "Passed";
                }
                else
                {
                    trVM.Status = "Failed";
                }

                return View("SeeResult", trVM);
        }

        public IActionResult getSessionAndCookie()
        {
            var d1 = HttpContext.Session.GetString("Trainee Name session");
            var d2 = HttpContext.Request.Cookies["CourseNamecookie"];

            return Content($"Data From Session : {d1}\n Data from Cookies {d2}");

        }
   
        public IActionResult TraineeDetails(int id)
        {

            var TraineeData = db.crsResult.Where(t=>t.Trainee.IsDeleted!=1)
                                       .Include(cr => cr.Trainee)
                                       .Include(cr => cr.Course)
                                       .FirstOrDefault(t => t.Trainee.ID == id);
            if(TraineeData==null)
            {
                return View  ("NotFoundTrainee");
            }
            var allCources = db.crsResult.Where(cr => cr.Traniee_id == id&&cr.IsDeleted!=1).Count();
            var allPassedCourse = db.crsResult.Include(cr => cr.Trainee)
                                             .Include(cr => cr.Course)
                                             .Where(cr => cr.degree >= cr.Course.minDegree &&cr.Traniee_id==id && cr.IsDeleted != 1)
                                             .Count();
            var allTraineeCources= db.crsResult.Where(cr => cr.IsDeleted != 1)
                                       .Include(cr => cr.Trainee)
                                       .Include(cr => cr.Course)
                                       .Where(t => t.Trainee.ID == id)
                                       .ToList();


            TraineeDetailsVM td = new TraineeDetailsVM();
            td.traineID = TraineeData.Traniee_id;
            td.traineAddress = TraineeData.Trainee.Address;
            td.traineName = TraineeData.Trainee.Name;
            td.traineDepartment = db.Trainee.Include(d => d.Department).FirstOrDefault(t => t.ID == id).Department.Name;
            td.traineTotalCources = allCources;
            td.trainePassedCources = allPassedCourse;
            td.traineFailedCources = allCources - allPassedCourse;
            td.traineSucessRate = allCources > 0 ? (allPassedCourse * 100) / allCources : 0;
            td.TraineeList = new List<TraineeCourcesData>();

            foreach(var item in allTraineeCources)
            {
                TraineeCourcesData tc = new TraineeCourcesData();
                tc.CourceName = item.Course.Name;
                tc.CourceDegree=item.Course.degree;
                tc.CourceID=item.Crs_id;
                tc.CourceGrade=item.degree;
                tc.CoursePersentageGrade=tc.CourceDegree>0? (tc.CourceGrade*100) / tc.CourceDegree : 0;
                tc.CoursMinDegree = item.Course.minDegree;
                tc.TNC = new TraineNameAndCourseVM();
                tc.TNC.CourseName = item.Course.Name;
                tc.TNC.TraineeName = item.Trainee.Name;
                if (tc.CourceGrade >= item.Course.minDegree)
                {
                    tc.CourceStatus = "Pass";
                    tc.Color = "Green";
                }else
                {
                    tc.CourceStatus = "Failed";
                    tc.Color = "Red";
                }
                var temp = db.instructor.Include(i => i.Course).FirstOrDefault(i => i.Crs_id == item.Crs_id);
                tc.CourceInstructor = temp != null ? temp.name : "N/A";


                td.TraineeList.Add(tc);
            }



            return View("TraineeDetails",td);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var TraineeData = db.Trainee.FirstOrDefault(c => c.ID == id);
            if (TraineeData != null)
            {
                TraineeData.IsDeleted = 1;
                db.Trainee.Update(TraineeData);
                db.SaveChanges();
            }   
            return RedirectToAction("Index");
        }

    }
}
