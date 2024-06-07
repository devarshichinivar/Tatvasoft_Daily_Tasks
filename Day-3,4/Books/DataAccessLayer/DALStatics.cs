using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALStatics
    {
        public static List<UserInfo> Users = new List<UserInfo>()
        {
            new UserInfo()
            {
                Username = "nisarg_admin",
                EmailAddress = "nisarg.admin@tatvasoft.com",
                Password = "Admin_pwd123",
                GivenName = "Nisarg",
                Surname = "Gami",
                Role = "Administrator"
            },

            new UserInfo()
            {
                Username = "john_reader",
                EmailAddress = "john.user@tatvasoft.com",
                Password = "john_pwd",
                GivenName = "John",
                Surname = "Doe",
                Role = "BookUser"
            },
        };
    }
}
