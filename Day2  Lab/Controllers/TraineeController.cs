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
        public IActionResult SeeResult(TraineNameAndCourseVM tc)
        {
            var resFromDB = db.crsResult.Include(cr => cr.Trainee)
                                      .Include(cr => cr.Course)
                                      .FirstOrDefault(cr => cr.Trainee.Name == tc.TraineeName && cr.Course.Name == tc.CourseName);
      
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
                if (trVM.Degree > trVM.Mindegree)
                {
                    trVM.Status = "Pass";
                    trVM.Color = "Green";
                }
                else
                {
                    trVM.Status = "Pass";
                    trVM.Color = "Green";
                }

                return View("SeeResult", trVM);
        }

        public IActionResult getSessionAndCookie()
        {
            var d1 = HttpContext.Session.GetString("Trainee Name session");
            var d2 = HttpContext.Request.Cookies["TraineeNamecookie"];

            return Content($"Data From Session : {d1}\n Data from Cookies {d2}");

        }
    }
}
