using System.Runtime.Intrinsics.Arm;
using Day2__Lab.Models;
using Day2__Lab.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day2__Lab.Controllers
{
    public class instructorController : Controller
    {
        //  /instructor/Index
        CompanyDbcontext db;

        public instructorController()
        {
            db = new CompanyDbcontext();
        }
        public IActionResult Index(int page = 1)
        {
            int pageSize = 4;
            int totalInstructor = db.instructor.Count();
            var _instroctors=db.instructor.Include(d=>d.Department)
                                         .OrderBy(i=>i.ID)
                                         .Skip((page - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToList();

            instructorPagination pagination = new instructorPagination()
            {
                instructors = _instroctors,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalInstructor
            };

            return View("Index", pagination);
        }
        public IActionResult Details(int id)
        {
            return View("Details",db.instructor.Include(d=>d.Department).FirstOrDefault(i=>i.ID==id));
        }
        public IActionResult Add()
        {
            return View ("Add");
        }
        [HttpPost]
        public IActionResult SaveData(instructor ins)
        {
            if (ins.name!=null)
            {
                db.instructor.Add(ins);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Add",ins);
        }

    }
}
