using CollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Repositories
{
    public interface ITeacherRepository
    {
        int Create(Teacher teacher);
        int Update(Teacher teacher);
        int Delete(int id);
        Teacher Get(int id);

        List<Teacher> GetAll();
        List<Teacher> GetTeacher(string tchSearch);
        Teacher GetTeacher(int userId);
    }
}
