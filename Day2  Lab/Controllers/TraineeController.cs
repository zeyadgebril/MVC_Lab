using System.Runtime.Intrinsics.Arm;
using Day2__Lab.Models;
using Day2__Lab.Repository;
using Day2__Lab.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day2__Lab.Controllers
{
    public class TraineeController : Controller
    {
        private readonly ITraineeRepository traineeRepository;
        private readonly IcrsResultRepository crsResultRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IInstructorRepository instructorRepository;

        //CompanyDbcontext db;
        public TraineeController(ITraineeRepository traineeRepository,
            IcrsResultRepository crsResultRepository,
            IDepartmentRepository departmentRepository,
            IInstructorRepository instructorRepository)
        {
            this.traineeRepository = traineeRepository;
            this.crsResultRepository = crsResultRepository;
            this.departmentRepository = departmentRepository;
            this.instructorRepository = instructorRepository;
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
            var temp = crsResultRepository.GetAll("Trainee,Course");
            var resFromDB=temp.FirstOrDefault(cr => cr.Trainee.Name == tc.TraineeName && cr.Course.Name == tc.CourseName);
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

            //var TraineeData = db.crsResult.Where(t=>t.Trainee.IsDeleted!=1)
            //                           .Include(cr => cr.Trainee)
            //                           .Include(cr => cr.Course)
            //                          
            var TraineeData=crsResultRepository.GetAll("Trainee,Course") .FirstOrDefault(t => t.Trainee.ID == id);
            var trainee = traineeRepository.GetAll("Department").FirstOrDefault(t => t.ID == id);

            if (trainee == null || trainee.Department == null)
            {
                return View  ("NotFoundTrainee");
            }
            var allCources = crsResultRepository.GetAll(string.Empty).Count();
            var allPassedCourse = crsResultRepository.TotalTraineePassed(id);
            var allTraineeCources = crsResultRepository.TotalTraineeCourses(id);


            TraineeDetailsVM td = new TraineeDetailsVM();
            td.traineID = TraineeData.Traniee_id;
            td.traineAddress = TraineeData.Trainee.Address;
            td.traineName = TraineeData.Trainee.Name;
            td.traineDepartment = trainee?.Department?.Name ?? "N/A";
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
                var temp = instructorRepository.GetAll("Course").FirstOrDefault(i => i.Crs_id == item.Crs_id);
                tc.CourceInstructor = temp != null ? temp.name : "N/A";
                td.TraineeList.Add(tc);
            }



            return View("TraineeDetails",td);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var condition = traineeRepository.Delete(id);
            if (condition)
            {
                traineeRepository.save();
                return Json(new { success = true });
            }
            else
            {
                return View("NotFound");
            }
        }

    }
}
