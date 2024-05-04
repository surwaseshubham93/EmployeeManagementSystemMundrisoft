using EMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.SqlServer.Server;

namespace EMS.Controllers
{
    public class LoginController : Controller
    {
        private readonly DatabaseContext _context;
        public LoginController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login login)
        {
            if (login == null)
            {

            }
            else
            {
                if (ModelState.IsValid)
                {
                    var res = _context.Register.Any(data => data.EmailId == login.EmailId && data.ConfirmPassword == login.Password);
                    if (res)
                    {
                        return RedirectToAction("Index", "EmpData");
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Registration reg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reg);
                int x = _context.SaveChanges();
                if(x > 0)
                {
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPass()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPass(ForgotPass pass)
        {
            if (pass == null)
            {

            }
            else
            {
                if (ModelState.IsValid)
                {
                    List<Model> list;
                    string sql = "exec sp_forgot_ems @email, @password, @cnfpassword";
                    List<SqlParameter> parameters = new List<SqlParameter>()
                    {
                        new SqlParameter{ParameterName = "@email", Value = Convert.ToString(pass.Email) },
                        new SqlParameter{ParameterName = "@password",Value=Convert.ToString(pass.Password)},
                        new SqlParameter{ParameterName = "@cnfpassword", Value=Convert.ToString(pass.ConfirmPassword)}
                    };
                    var res = _context.Database.ExecuteSqlRaw(sql, parameters.ToArray());
                    if (res > 0)
                    {
                        TempData["Success"] = "Password Updated Successfully!";
                        return RedirectToAction("Login", "Login");
                    }
                }
            }
            return View();
        }
    }
}
