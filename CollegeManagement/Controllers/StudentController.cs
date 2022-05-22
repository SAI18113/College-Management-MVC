using ClosedXML.Excel;
using CollegeManagement.Data;
using CollegeManagement.Models;
using CollegeManagement.Models.ViewModels;
using CollegeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudentController(IStudentService studentService, ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _studentService = studentService;
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        //GET REGISTER
        [Authorize(Roles ="admin")]
        public IActionResult Register()
        {
            return View();
        }
        //POST REGISTER
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(StudentViewModel student)
        {
                if (ModelState.IsValid)
                {
                    _studentService.Save(student);
                    return RedirectToAction("GetAllDetails");
                }
            return RedirectToAction("Index", "Home");
        }



        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllDetails(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
           
                ViewData["CurrentSort"] = sortOrder;
                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewData["CurrentFilter"] = searchString;
                var student = from s in _db.Students select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    student = student.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "name_desc":
                        student = student.OrderByDescending(s => s.FirstName);
                        break;
                    default:
                        student = student.OrderBy(s => s.FirstName);
                        break;
                }
                int pageSize = 5;
                return View(await PaginatedList<Student>.CreateAsync(student.AsNoTracking(), pageNumber ?? 1, pageSize));
            

        }


        [Authorize(Roles = "admin")]
        public IActionResult GetUpdate(int Id)
        {
           
                var student = _studentService.Get(Id);
                return View(student);
            
        }
        [Authorize(Roles = "admin")]
        public IActionResult UpdatePost(Student student)
        { 
                _studentService.Edit(student);
                return RedirectToAction("GetAllDetails");   
        }
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int Id)
        {
          
                _studentService.Delete(Id);
                return RedirectToAction("GetAllDetails");
            

        }
        [Authorize(Roles = "Student")]
        public ActionResult Details(int userId)
        {
            
                ViewBag.userId = TempData["user"];
                userId = ViewBag.userId;
                _studentService.GetStudent(userId);
                ViewData["student"] = _studentService.GetStudent(userId);
                return View();
            
            

        }
      


    }
}
