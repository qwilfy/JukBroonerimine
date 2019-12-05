using jukbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jukbook.Controllers
{
    public class UserController : ApiController
    {
        private BroonContext db = new BroonContext();

        public LoginResponce Post(RegisterUser registerUser)
        {
            LoginResponce result = new LoginResponce();

            if (registerUser.email.Trim().Length == 0)
            {
                result.ResultCode = 401;
                result.ResultMessage = "Поле почты не должно быть пустым";
            }
            else if (registerUser.password.Trim().Length == 0 || registerUser.password_second.Trim().Length == 0)
            {
                result.ResultCode = 401;
                result.ResultMessage = "Поля паролей не должны быть пусты";
            }
            else if (registerUser.password != registerUser.password_second)
            {
                result.ResultCode = 401;
                result.ResultMessage = "Пароли не совпадают";
            }
            else if (db.Users.Where(x => x.login == registerUser.email).FirstOrDefault() != null)
            {
                result.ResultCode = 401;
                result.ResultMessage = "Данная почта уже используется";
            }
            else
            {
                string password = registerUser.password;

                User user = new User();
                user.login = registerUser.email;
                user.password = Hash.ComputeSha256Hash(password);
                user.roleID = 1;
                user.name = registerUser.name;

                db.Users.Add(user);
                db.SaveChanges();

                result.ResultCode = 200;
            }
            return result;
        }
    }
}
