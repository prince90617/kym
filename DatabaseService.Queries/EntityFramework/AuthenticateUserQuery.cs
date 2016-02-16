using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseService.Models;


namespace DatabaseService.Queries.EntityFramework
{
    public static class AuthenticateUserQuery
    {
        public static DatabaseService.Models.User AuthenticateUser(string username)
        {
            try
            {
                using (var ctx = new SMContext())
                {
                    var user = from u in ctx.Users
                               join r in ctx.Roles on u.role_id equals r.role_id
                               where u.username == username && u.status == 1
                               select new DatabaseService.Models.User { Id = u.user_id, Username = u.username, Photo = string.IsNullOrEmpty(u.photo) ? "DefaultPic.jpg" : u.photo, RoleName = r.role, Email = u.email, HashedPassword = u.password, Name = u.name, Active = u.status };

                    return user.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                BaseClass.logger.Error("Database Query AuthenticateUser: ", ex);
                return null;
            }
        }

    }
}
