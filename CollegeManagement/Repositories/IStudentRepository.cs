using CollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Repositories
{
    public interface IStudentRepository
    {
        int Create(Student student);
        int Update(Student student);
        int Delete(int Id);
        Student Get(int id);
        List<Student> GetAll();
        List<Student> GetStudent(string stdSearch);
        Student GetStudent(int userId);
    }
}
