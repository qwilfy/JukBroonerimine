using jukbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace jukbook.Controllers
{
    public class LoginController : ApiController
    {
        private BroonContext db = new BroonContext();
        public LoginResponce Post(LoginUser user)
        {
            LoginResponce result = new LoginResponce();

            if (user.password == "")
            {
                result.ResultCode = 401;
                result.ResultMessage = "Поле пароля не должен быть пусты";
            }
            else if (user.email == "")
            {
                result.ResultCode = 401;
                result.ResultMessage = "Поле почты не должно быть пустым";
            }
            else
            {
                User dbUser = db.Users.Where(x => x.login == user.email).FirstOrDefault();
                if (dbUser != null)
                {
                    string password = user.password;
                    password = Hash.ComputeSha256Hash(password);
                    if (dbUser.password == password)
                    {
                        HttpContext.Current.Session["userId"] = dbUser.ID;
                        HttpContext.Current.Session["email"] = user.email;
                        HttpContext.Current.Session["roleid"] = dbUser.roleID;
                        HttpContext.Current.Session["name"] = dbUser.name;
                        HttpContext.Current.Session["login"] = dbUser.login;

                        result.ResultCode = 200;
                        //result = "Success";
                    }
                    else
                    {
                        result.ResultCode = 401;
                        result.ResultMessage = "Неверный пароль";
                    }
                }
                else
                {
                    result.ResultCode = 401;
                    result.ResultMessage = "Неверная почта";
                }
            }
            return result;
        }
    }
}
