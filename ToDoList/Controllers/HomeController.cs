using Microsoft.AspNetCore.Mvc;
using RealEstateWebsite.Models.Admin;
using RealEstateWebsite.Models.Home;
using System.Diagnostics;
using ToDoList.Models;
using static RealEstateWebsite.Repository.Admin.AdminUpdatePasswordRespository;
using static RealEstateWebsite.Repository.Home.UpdatePasswordRepository;
using static ToDoList.Repository.RegistrationRepository;
using static ToDoList.Repository.TaskRepository;


namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRegister _register;
        private readonly ITask _task;
        private readonly IForgetPasswordService _updatePasswordService;
        private readonly IAdminUpdatePasswordService _adminUpdatePasswordService;
        public HomeController(IRegister register, ITask task, IForgetPasswordService updatePasswordService, IAdminUpdatePasswordService adminUpdatePasswordService)
        {
            _register = register;
            _task = task;
            _updatePasswordService = updatePasswordService;
            _adminUpdatePasswordService = adminUpdatePasswordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(RegistrationModel registrationModel)
        {
            registrationModel.EmailCheck = string.IsNullOrEmpty(registrationModel.Email);
            registrationModel.PasswordCheck = string.IsNullOrEmpty(registrationModel.Password);
            _register.Login(registrationModel);
            if (registrationModel.EmailCheck == true && registrationModel.PasswordCheck == true)
            {
                Response.Cookies.Delete("UserCookie");
                return View();
            }
            else if (registrationModel.Temp <= 0)
            {
                ViewBag.Message = registrationModel.Message;
                ViewBag.Color = registrationModel.Color;
                Console.WriteLine(registrationModel.IDCookieUser.ToString());
                ViewBag.Cookie = registrationModel.IDCookieUser.ToString();
                Response.Cookies.Append("UserCookie", ViewBag.Cookie);
                return View();
            }
            else
            {
                _register.Login(registrationModel);
                ViewBag.Message = registrationModel.Message;
                ViewBag.Color = registrationModel.Color;
                Console.WriteLine(registrationModel.IDCookieUser.ToString());
                ViewBag.Cookie = registrationModel.IDCookieUser.ToString();
                Response.Cookies.Append("UserCookie", ViewBag.Cookie);
                return RedirectToAction("UserDashboard");
            }
        }
        public IActionResult Registration(RegistrationModel registrationModel)
        {
            registrationModel.FirstNameCheck = string.IsNullOrEmpty(registrationModel.FirstName);
            registrationModel.LastNameCheck = string.IsNullOrEmpty(registrationModel.LastName);
            registrationModel.EmailCheck = string.IsNullOrEmpty(registrationModel.Email);
            registrationModel.PasswordCheck = string.IsNullOrEmpty(registrationModel.Password);
            registrationModel.CPasswordCheck = string.IsNullOrEmpty(registrationModel.CPassword);

            //Register Page
            if (registrationModel.FirstNameCheck == true && registrationModel.LastNameCheck == true && registrationModel.EmailCheck == true && registrationModel.PasswordCheck == true && registrationModel.CPasswordCheck == true)
            {
                return View();
            }
            else if (registrationModel.Password != registrationModel.CPassword)
            {
                ViewBag.Message = "Passwords Doesnt Match!!!";
                ViewBag.Color = "Red";
                return View();
            }
            else
            {
                _register.Registration(registrationModel);
                ViewBag.Message = registrationModel.Message;
                ViewBag.Color = registrationModel.Color;
                return View();
            }

        }

        public IActionResult UserDashboard(TaskModel taskss)
        {
            taskss.TaskCheck = string.IsNullOrEmpty(taskss.TaskData);
            taskss.TaskDCheck = string.IsNullOrEmpty(taskss.deletetask);
            taskss.TaskUCheck = string.IsNullOrEmpty(taskss.Updatetask);
            taskss.TaskDDCheck = string.IsNullOrEmpty(taskss.tasdID);
            taskss.TaskACheck = string.IsNullOrEmpty(taskss.addtasks);
            taskss.TaskTCheck = string.IsNullOrEmpty(taskss.TaskTime);
            taskss.TaskDTimecheck = string.IsNullOrEmpty(taskss.UpdatetaskTime);
            if (taskss.TaskDCheck == true && taskss.TaskUCheck == true && taskss.TaskCheck == true && taskss.TaskDDCheck == true && taskss.TaskACheck == true && taskss.TaskTCheck == true && taskss.TaskDTimecheck == true)
            {
                taskss.UserID = Request.Cookies["UserCookie"].ToString();
                _task.FetchTasks(taskss);
                ViewBag.Display = taskss.list2;
                _task.UpdateTaskFunction(taskss);
                return View();
            }
            else if (taskss.TaskDCheck == false)
            {
                taskss.UserID = Request.Cookies["UserCookie"].ToString();
                Console.WriteLine(2 + taskss.deletetask);
                _task.DeleteTaskFunction(taskss);
                _task.FetchTasks(taskss);
                _task.UpdateTaskFunction(taskss);
                ViewBag.Display = taskss.list2;
                ViewBag.Message = taskss.Message;
                return View();
            }
            else if (taskss.TaskUCheck == false && taskss.TaskDDCheck == false)
            {
                taskss.UserID = Request.Cookies["UserCookie"].ToString();
                Console.WriteLine(3 + taskss.deletetask);
                _task.UpdateTaskFunction(taskss);
                _task.FetchTasks(taskss);
                ViewBag.Display = taskss.list2;
                ViewBag.Message = taskss.Message;
                return View();
            }
            else if (Request.Form.ContainsKey("addtasks"))
            {
                taskss.UserID = Request.Cookies["UserCookie"].ToString();
                Console.WriteLine(4 + taskss.deletetask);
                _task.TaskFunction(taskss);
                _task.UpdateTaskFunction(taskss);
                _task.FetchTasks(taskss);
                ViewBag.Display = taskss.list2;
                ViewBag.Message = taskss.Message;
                return View();
            }
            else
            {
                taskss.UserID = Request.Cookies["UserCookie"].ToString();
                _task.FetchTasks(taskss);
                ViewBag.Display = taskss.list2;
                return View();
            }
         
        }

        //Forget Password
        public IActionResult ForgetPassword(UpdatePasswordModel updatePasswordModel)
        {
            bool X = string.IsNullOrEmpty(updatePasswordModel.ForgetPassword);
            if (X == true)
            {
                return View();
            }
            else
            {
                _updatePasswordService.ForgetPassword(updatePasswordModel);
                ViewBag.ForgetPassword = updatePasswordModel.Message;
                ViewBag.ForgetPassword2 = updatePasswordModel.Message2;
                return View();
            }

        }

        //Update Profile 
        public IActionResult UpdateProfiles(AdminUpdatePasswordModel adminUpdatePasswordModel)
        {
            //ViewBag.NotificationBag = Request.Cookies["MyCookieNotification"].ToString();
            bool old = string.IsNullOrEmpty(adminUpdatePasswordModel.AdminOldPassword), neww = string.IsNullOrEmpty(adminUpdatePasswordModel.AdminNewPassword), conf = string.IsNullOrEmpty(adminUpdatePasswordModel.AdminCNewPassword);
            if (Request.Cookies["UserCookie"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (old == true)
            {
                return View();
            }
            else
            {
                adminUpdatePasswordModel.ID = Request.Cookies["UserCookie"].ToString();
                _adminUpdatePasswordService.Update(adminUpdatePasswordModel);
                ViewBag.Info = adminUpdatePasswordModel.Msg;
                ViewBag.color = adminUpdatePasswordModel.color;
                return View();
            }
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserCookie");
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}