using CollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Repositories
{
    public interface IUserRepository
    {
        int Create(User user);
        User Get(string emailId, string password);
        User Get(string emailId);
    }
}
