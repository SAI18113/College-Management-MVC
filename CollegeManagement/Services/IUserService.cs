using CollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Services
{
    public interface IUserService
    {
        bool Save(User user);
        bool IsValidUser(string emailId, string password);
        User Get(string EmailId);
  
    }
}
