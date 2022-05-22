using CollegeManagement.Models;
using CollegeManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userrepository;
        public UserService(IUserRepository userrepository)
        {
            _userrepository = userrepository;
        }
        public bool Save(User user)
        {
            if (string.IsNullOrEmpty(user.EmailId))
            {
                throw new Exception("EmailId Should Not Be Empty");
            }
            int rowsaffected = _userrepository.Create(user);
            return rowsaffected > 0;
        }
        public bool IsValidUser(string emailId, string password)
        {
            var u = _userrepository.Get(emailId, password);
            return u != null;
        }

        public User Get(string EmailId)
        {
            var user = _userrepository.Get(EmailId);
            return user;
        }


    }
}
