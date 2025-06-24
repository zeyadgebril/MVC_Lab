using System.Runtime.Intrinsics.Arm;
using Day2__Lab.Models;
using Day2__Lab.Repository;
using Day2__Lab.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day2__Lab.Controllers
{
    [Authorize(Roles ="admin")]
    public class instructorController : Controller
    {
        private readonly IInstructorRepository instructorRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IDepartmentRepository departmentRepository;
        public instructorController(IInstructorRepository instructorRepository, ICourseRepository courseRepository,IDepartmentRepository departmentRepository)
        {
            this.instructorRepository = instructorRepository;
            this.courseRepository = courseRepository;
            this.departmentRepository = departmentRepository;
        }
        public IActionResult Index(int page = 1)
        {
            int pageSize = 4;
            int totalInstructor = instructorRepository.NumberOfInstructor();
            var _instroctors=instructorRepository.GetWithPagintion("Department",page,pageSize);


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
            
            return View("Details", instructorRepository.GetAll("Department").FirstOrDefault(i => i.ID == id));
        }
        public IActionResult Add()
        {
            InstructorWithDeptList Idepptlist= new InstructorWithDeptList()
            {
                CourseList = courseRepository.GetAll(string.Empty) ,
                DeptList = departmentRepository.GetAll(string.Empty),

            };
            
            return View ("Add",Idepptlist);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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

                instructorRepository.Add(instructor);
                instructorRepository.save();
                return RedirectToAction ("Index");
            }

            return View("Add", ins);
        }


        public IActionResult Edit(int id)
        {
            var dataOfInstructor = instructorRepository.GetById(id);
            InstructorWithDeptList Idepptlist = new InstructorWithDeptList()
            {
                ID=dataOfInstructor.ID,
                name = dataOfInstructor.name,
                salary=dataOfInstructor.salary,
                Dept_id=dataOfInstructor.Dept_id,
                Crs_id=dataOfInstructor.Crs_id,
                Image=dataOfInstructor.Image,
                Address=dataOfInstructor.Address,
                CourseList = courseRepository.GetAll(string.Empty),
                DeptList = departmentRepository.GetAll(string.Empty),

            };
            return View("Edit", Idepptlist);
        }

        public IActionResult SaveEdit(InstructorWithDeptList ins)
        {
            if (ModelState.IsValid)
            {
                var insFromDB = instructorRepository.GetById(ins.ID);
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
                    instructorRepository.Update(insFromDB);
                    instructorRepository.save();
                    return RedirectToAction("Index");
                }
            }
            ins.CourseList = courseRepository.GetAll(string.Empty);
            ins.DeptList = departmentRepository.GetAll(string.Empty);
            return View("Edit", ins);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var condition = instructorRepository.Delete(id);
            if (condition)
            {
                instructorRepository.save();
                return Json(new { success = true });
            }
            else
            {
                return View("NotFound");
            }
        }
        //instructor/GetCourseByDeptId?deptID=
        public IActionResult GetCourseByDeptId(int deptID)
        {
            var data = courseRepository.GetAll(string.Empty).Where(c => c.Dept_id == deptID).Select(c => new { c.ID, c.Name }).ToList();
            return Json(data);
        }


    }
}
