using CollegeManagement.Models;
using CollegeManagement.Models.ViewModels;
using CollegeManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentrepository;
        private readonly IUserRepository _userRepository;
        public StudentService(IStudentRepository studentrepository, IUserRepository userRepository)
        {
            _studentrepository = studentrepository;
            _userRepository = userRepository;
        }

        public bool Edit(Student student)
        {

            var obj = _studentrepository.Update(student);
            return obj > 0;

        }

        public List<Student> GetDetails()
        {
            List<Student> obj = new List<Student>();
            obj = _studentrepository.GetAll();
            return obj;
        }

        public Student Get(int Id)
        {
            var student = _studentrepository.Get(Id);
            var std = new Student()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Adress = student.Adress,
                Mobile = student.Mobile,
                Age = student.Age,
                State = student.State,
                City = student.City
            };
            return std;
        }
         

        public bool Save(StudentViewModel student)
        {
            var std = new Student()
            {

                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Adress = student.Adress,
                Mobile=student.Mobile,
                Age=student.Age,
                City = student.City,
                State = student.State,
            };
            if (student.Age < 5)
            {
                throw new Exception("Age must be greater than  or equal to 5");
            }
            if (student.City.ToLower() != "vizag")
            {
                throw new Exception("Student must be from vizag city");
            }
            int rowsAffected = 0;
            if (student.Id > 0)
            {
                rowsAffected = _studentrepository.Update(std);
            }
            else
            {
                var user = new User { EmailId = student.EmailId, UserType = "Student", UserPassword = "Miracle@123", IsActive = true };
                _userRepository.Create(user);
                std.UserId = user.Id;
                user = _userRepository.Get(user.EmailId);
                rowsAffected = _studentrepository.Create(std);
            }
            return rowsAffected > 0;
        }

        public bool Delete(int Id)
        {
            int affectedRows = 0;
            if (Id > 0)
            {
                affectedRows = _studentrepository.Delete(Id);
            }
            return affectedRows > 0;
        }

        public List<Student> GetStudent(string stdSearch)
        {
            var student = _studentrepository.GetStudent(stdSearch);
            return student;
        }

        public Student GetStudent(int userId)
        {
            var student = _studentrepository.GetStudent(userId);
            var std = new Student()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName=student.LastName,
                Adress=student.Adress,
                Mobile=student.Mobile,
                Age=student.Age,
                State=student.State,
                City=student.City

            };
            return std;
        }
    }
}
