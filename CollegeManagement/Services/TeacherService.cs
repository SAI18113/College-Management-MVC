using CollegeManagement.Models;
using CollegeManagement.Models.ViewModels;
using CollegeManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepositry;
        private readonly IUserRepository _userRepository;
        public TeacherService(ITeacherRepository teacherRepository, IUserRepository userRepository)
        {
            _teacherRepositry = teacherRepository;
            _userRepository = userRepository;
        }
        public bool Delete(int Id)
        {
            int affectedRows = 0;
            if (Id > 0)
            {
                affectedRows = _teacherRepositry.Delete(Id);
            }
            return affectedRows > 0;
        }


        public List<Teacher> GetDetails()
        {
            List<Teacher> obj = new List<Teacher>();
            obj = _teacherRepositry.GetAll();
            return obj;
        }

        public bool Save(TeacherViewModel teacher)
        {
            var tch = new Teacher()
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Adress = teacher.Adress,
                Mobile=teacher.Mobile,
                Age=teacher.Age,
                City = teacher.City,
                State = teacher.State
            };
            int rowsAffected = 0;
            if (teacher.Id > 0)
            {
                rowsAffected = _teacherRepositry.Update(tch);
            }
            else
            {
                var user = new User { EmailId = teacher.EmailId, UserType = "Teacher", UserPassword = "Miracle@123", IsActive = true };
                _userRepository.Create(user);
                tch.UserId = user.Id;
                user = _userRepository.Get(user.EmailId);
                rowsAffected = _teacherRepositry.Create(tch);
            }
            return rowsAffected > 0;
        }

        public bool Edit(Teacher teacher)
        {
            var obj = _teacherRepositry.Update(teacher);
            return obj > 0;
        }

        public Teacher Get(int Id)
        {
            var teacher = _teacherRepositry.Get(Id);
            var tch = new Teacher()
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Adress = teacher.Adress,
                Mobile = teacher.Mobile,
                Age=teacher.Age,
                State = teacher.State,
                City = teacher.City
            };
            return tch;
        }

        public List<Teacher> GetTeacher(string tchSearch)
        {
            var teacher = _teacherRepositry.GetTeacher(tchSearch);
            return teacher;
        }

        public Teacher GetTeacher(int userId)
        {
            var teacher = _teacherRepositry.GetTeacher(userId);
            Teacher tch = new Teacher()
            {
                Id = teacher.Id,
                FirstName=teacher.FirstName,
                LastName=teacher.LastName,
                Adress=teacher.Adress,
                Mobile=teacher.Mobile,
                Age=teacher.Age,
                State=teacher.State,
                City=teacher.City
            };
            return tch;
        }
    }
}
