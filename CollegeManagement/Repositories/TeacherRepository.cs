using CollegeManagement.Data;
using CollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _db;
        public TeacherRepository(ApplicationDbContext db)
        {
            _db = db;


        }
        public int Create(Teacher teacher)
        {
            _db.Teachers.Add(teacher);
            int rowsAffected = _db.SaveChanges();
            return rowsAffected;
        }
        public int Update(Teacher teacher)
        {
            var s = _db.Teachers.Where(i => i.Id == teacher.Id).FirstOrDefault();
            s.FirstName = teacher.FirstName;
            s.LastName = teacher.LastName;
            s.Adress = teacher.Adress;
            s.Age = teacher.Age;
            s.Mobile = teacher.Mobile;
            s.City = teacher.City;
            s.State = teacher.State;
            //s = _mapper.Map<Student>(student);

            _db.Update(s);
            int rowsAffected = _db.SaveChanges();
            return rowsAffected;

        }
        public int Delete(int id)
        {
            var teacher = _db.Teachers.Where(i => i.Id == id).FirstOrDefault();
            _db.Remove(teacher);
            int rowsAffected = _db.SaveChanges();
            return rowsAffected;

        }
        public Teacher Get(int id)
        {
            var teacher = _db.Teachers.Where(i => i.Id == id).FirstOrDefault();
            return teacher;
        }
        public List<Teacher> GetAll()
        {
            var teacher = _db.Teachers.ToList();
            return teacher;
        }

        public List<Teacher> GetTeacher(string tchSearch)
        {
            List<Teacher> stdquery = new List<Teacher>();
            if (!string.IsNullOrEmpty(tchSearch))
            {
                stdquery = _db.Teachers.Where(x => x.FirstName.Contains(tchSearch) || x.LastName.Contains(tchSearch)).ToList();

            }
            else
            {
                stdquery = (from x in _db.Teachers select x).ToList();

            }
            return stdquery.ToList();
        }

        public Teacher GetTeacher(int userId)
        {
            var teacher = _db.Teachers.Where(i => i.UserId == userId).FirstOrDefault();
            return teacher;
        }
    }
}
