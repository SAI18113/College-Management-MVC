using ClosedXML.Excel;
using CollegeManagement.Data;
using CollegeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeManagement.Controllers
{
    [Authorize(Roles="admin")]
    public class ExportController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ExportController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7] {
            new DataColumn("First Name"),
            new DataColumn("Last Name"),
            new DataColumn("Address"),
            
            new DataColumn("Mobile"),
            new DataColumn("Age"),
            new DataColumn("State"),
            new DataColumn("City")
            });
            var stds = from std in _db.Students.ToList() select std;
            foreach (var std in stds)
            {
                dt.Rows.Add(std.FirstName, std.LastName, std.Adress, std.Mobile, std.Age, std.State, std.City);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }
        [HttpPost]
        public FileResult ExportT()
        {
            DataTable dt = new DataTable("GridT");
            dt.Columns.AddRange(new DataColumn[7] {
            new DataColumn("First Name"),
            new DataColumn("Last Name"),
            new DataColumn("Address"),
            
            new DataColumn("Mobile"),
            new DataColumn("Age"),
            new DataColumn("State"),
            new DataColumn("City")
            });
            var stds = from std in _db.Teachers.ToList() select std;
            foreach (var std in stds)
            {
                dt.Rows.Add(std.FirstName, std.LastName, std.Adress, std.Mobile,std.Age, std.State, std.City);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GridT.xlsx");
                }
            }
        }
       [HttpPost]
       public FileResult ExportCSV()
        {
            var student = _db.Students.ToList();
            StringWriter sw = new StringWriter();
            sw.WriteLine("\"First Name\",\"Last Name\",\"Address\",\"Mobile\",\"Age\",\"State\",\"City\"");
            foreach(var std in student)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"",std.FirstName,std.LastName,std.Adress,
                    std.Mobile,std.Age,std.State,std.City));
            }
           
            return File(Encoding.ASCII.GetBytes(sw.ToString()), "text/csv", "StudentList");


        }
        [HttpPost]
        public FileResult ExportCSVT()
        {
            var teacher = _db.Teachers.ToList();
            StringWriter sw = new StringWriter();
            sw.WriteLine("\"First Name\",\"Last Name\",\"Address\",\"Mobile\",\"Age\",\"State\",\"City\"");
            foreach (var tch in teacher)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"", tch.FirstName, tch.LastName, tch.Adress,
                    tch.Mobile, tch.Age, tch.State, tch.City));
            }

            return File(Encoding.ASCII.GetBytes(sw.ToString()), "text/csv", "TeacherList");


        }
    }
}
