using CollegeManagement.Data;
using CollegeManagement.Models;
using CollegeManagement.Models.ViewModels;
using CollegeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly ApplicationDbContext _db;
        public TeacherController(ITeacherService teacherService, ApplicationDbContext db)
        {
            _teacherService = teacherService;
            _db = db;
        }
        [Authorize(Roles ="admin")]
        public ActionResult Register()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(TeacherViewModel teacher)
        {
            if (ModelState.IsValid)
            {
                _teacherService.Save(teacher);


            }
            return RedirectToAction("GetAllDetails");

        }
        [Authorize(Roles = "admin")]
        public IActionResult GetUpdate(int Id)
        {
            var teacher = _teacherService.Get(Id);
            return View(teacher);
        }
        [Authorize(Roles = "admin")]
        public IActionResult UpdatePost(Teacher teacher)
        {
            _teacherService.Edit(teacher);
            return RedirectToAction("GetAllDetails");
        }
       
      
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            _teacherService.Delete(id);
            return RedirectToAction("GetAllDetailsT");
        }
        [Authorize(Roles = "Teacher")]
        public ActionResult Details(int userId)
        {
            ViewBag.userId = TempData["user"];
            userId = ViewBag.userId;
            _teacherService.GetTeacher(userId);
            ViewData["teacher"] = _teacherService.GetTeacher(userId);
            return View();
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
            var teacher = from s in _db.Teachers select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                teacher = teacher.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "name_desc":
                    teacher = teacher.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    teacher = teacher.OrderBy(s => s.FirstName);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Teacher>.CreateAsync(teacher.AsNoTracking(), pageNumber ?? 1, pageSize));


        }
    }
}
