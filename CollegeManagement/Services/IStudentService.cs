using CollegeManagement.Models;
using CollegeManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Services
{
    public interface IStudentService
    {
        bool Save(StudentViewModel student);
        List<Student> GetDetails();

        bool Edit(Student student);
        Student Get(int Id);
        bool Delete(int Id);
        List<Student> GetStudent(string stdSearch);
        Student GetStudent(int userId);
    }
}
