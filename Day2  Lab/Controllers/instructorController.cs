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
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (ins.ImageFile != null && ins.ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + ins.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ins.ImageFile.CopyTo(fileStream);
                    }
                }

                var instructor = new instructor
                {
                    name = ins.name,
                    salary = ins.salary,
                    Dept_id = ins.Dept_id,
                    Crs_id = ins.Crs_id,
                    Address = ins.Address,
                    Image = uniqueFileName 
                };

                db.instructor.Add(instructor);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Add", ins);
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
                var insFromDB = db.instructor.FirstOrDefault(i => i.ID == ins.ID);
                if (insFromDB != null)
                {
                    insFromDB.name = ins.name;
                    insFromDB.salary = ins.salary;
                    insFromDB.Address = ins.Address;
                    insFromDB.Dept_id = ins.Dept_id;
                    insFromDB.Crs_id = ins.Crs_id;

                    if (ins.ImageFile != null)
                    {
                        string imageName = Guid.NewGuid().ToString() + Path.GetExtension(ins.ImageFile.FileName);
                        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image", imageName);

                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            ins.ImageFile.CopyTo(stream);
                        }
                        insFromDB.Image = imageName;
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ins.CourseList = db.Course.ToList();
            ins.DeptList = db.Department.ToList();
            return View("Edit", ins);
        }


    }
}
