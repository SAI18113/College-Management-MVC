using CollegeManagement.Data;
using CollegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollegeManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public int Create(User user)
        {
            _db.Users.Add(user);
            int rowsAffected = _db.SaveChanges();
            return rowsAffected;
        }
        public User Get(string emailId, string password)
        {
            var user = _db.Users.Where(i => i.EmailId == emailId && i.UserPassword == password).FirstOrDefault();
            return user;
        }

        public User Get(string emailId)
        {
            var user = _db.Users.Where(i => i.EmailId == emailId).FirstOrDefault();
            return user;
        }
    }
}
