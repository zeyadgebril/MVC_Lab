using System.Runtime.Intrinsics.Arm;
using Day2__Lab.Models;
using Day2__Lab.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day2__Lab.Controllers
{
    public class instructorController : Controller
    {
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
            InstructorWithDeptList Idepptlist= new InstructorWithDeptList()
            {
                CourseList = db.Course.ToList(),
                DeptList = db.Department.ToList(),

            };
            
            return View ("Add",Idepptlist);
        }
        [HttpPost]
        public IActionResult SaveData(InstructorWithDeptList ins)
        {
            //ModelState.Remove("DeptList");
            //ModelState.Remove("CourseList");

            if (ModelState.IsValid)
            {
                instructor instructor = new instructor();
                instructor.name = ins.name;
                instructor.salary = ins.salary;
                instructor.Dept_id = ins.Dept_id;
                instructor.Crs_id = ins.Crs_id;
                instructor.Image = ins.Image;
                instructor.Address = ins.Address;

                db.instructor.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Add",ins);
        }


        public IActionResult Edit(int id)
        {
            var dataOfInstructor = db.instructor.FirstOrDefault(i => i.ID == id);
            InstructorWithDeptList Idepptlist = new InstructorWithDeptList()
            {
                ID=dataOfInstructor.ID,
                name = dataOfInstructor.name,
                salary=dataOfInstructor.salary,
                Dept_id=dataOfInstructor.Dept_id,
                Crs_id=dataOfInstructor.Crs_id,
                Image=dataOfInstructor.Image,
                Address=dataOfInstructor.Address,
                CourseList = db.Course.ToList(),
                DeptList = db.Department.ToList(),

            };
            return View("Edit", Idepptlist);
        }

        public IActionResult SaveEdit(InstructorWithDeptList ins)
        {
            if (ModelState.IsValid)
            {
                var insFromDB = db.instructor.FirstOrDefault(i=>i.ID==ins.ID);
                insFromDB.name = ins.name;
                insFromDB.salary = ins.salary;
                insFromDB.Address = ins.Address;
                insFromDB.Dept_id=ins.Dept_id;
                insFromDB.Crs_id=ins.Crs_id;
                insFromDB.Image = ins.Image;

                db.instructor.Update(insFromDB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit",ins);
        }
    }
}
