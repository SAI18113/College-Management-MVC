using CollegeManagement.Data;
using CollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _db;
        public StudentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public int Create(Student student)
        {
            _db.Students.Add(student);
            int rowsAffected = _db.SaveChanges();
            return rowsAffected;
        }
        public int Update(Student student)
        {
            var s = _db.Students.Where(x => x.Id == student.Id).FirstOrDefault();
            s.FirstName = student.FirstName;
            s.LastName = student.LastName;
            s.Adress = student.Adress;
            s.Age = student.Age;
            s.Mobile = student.Mobile;
            s.State = student.State;
            s.City = student.City;
            _db.Students.Update(s);
            int rowsAffected = _db.SaveChanges();
            return rowsAffected;
        }
        public int Delete(int id)
        {
            var student = _db.Students.Where(x => x.Id == id).FirstOrDefault();
            _db.Students.Remove(student);
            int rowsAffected = _db.SaveChanges();
            return rowsAffected;
        }
        public Student Get(int id)
        {
            var student = _db.Students.Where(x => x.Id == id).FirstOrDefault();
            return student;
        }
        public List<Student> GetAll()
        {
            var students = _db.Students.ToList();
            return students;
        }
        public List<Student> GetStudent(string stdSearch)
        {
            List<Student> stdquery = new List<Student>();
            if (!string.IsNullOrEmpty(stdSearch))
            {
                stdquery = _db.Students.Where(x => x.FirstName.Contains(stdSearch) || x.LastName.Contains(stdSearch) || x.Mobile.Contains(stdSearch)).ToList();

            }
            else
            {
                stdquery = (from x in _db.Students select x).ToList();

            }
            return stdquery.ToList();
        }
        public Student GetStudent(int userId)
        {
            var student = _db.Students.Where(i => i.UserId == userId).FirstOrDefault();
            return student;
        }


    }
}
