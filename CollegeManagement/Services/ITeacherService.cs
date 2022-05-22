using CollegeManagement.Models;
using CollegeManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Services
{
    public interface ITeacherService
    {
        bool Save(TeacherViewModel teacher);
        List<Teacher> GetDetails();

        bool Edit(Teacher teacher);
        Teacher Get(int Id);
        bool Delete(int Id);
        List<Teacher> GetTeacher(string tchSearch);
        Teacher GetTeacher(int userId);
    }
}
