using System.Data;
using System.Linq;
using System.Web.Mvc;
using User_Management_with_login_Register.Context;

namespace User_Management_with_login_Register.Controllers
{
    public class UserController : Controller
    {
        public TechBroEntities _dbContext;

        public UserController()
        {
            _dbContext = new TechBroEntities();
        }

        // GET: User/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = _dbContext.User_info.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                Session["UserName"] = user.Name;
                Session["UserRole"] = user.Role;
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid email or password";
            return View();
        }

        // GET: User/Register
        [HttpGet]
        public ActionResult Register()
        {
            ModelState.Clear();
            return View();
        }

        // POST: User/Register
        [HttpPost]
        public ActionResult Register(User_info user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.User_info.Add(user);
                _dbContext.SaveChanges();
                TempData["RegisterSuccess"] = "User registered successfully!";
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // GET: User/Dashboard
        public ActionResult Dashboard()
        {
            if (Session["UserName"] == null)
                return RedirectToAction("Login");

            var users = _dbContext.User_info.ToList();

            ViewBag.Name = Session["UserName"];
            ViewBag.Role = Session["UserRole"];
            return View(users); // pass users to the view
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = _dbContext.User_info.Find(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User_info updatedUser)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(updatedUser).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }

            return View(updatedUser);
        }

        public ActionResult Delete(int id)
        {
            var user = _dbContext.User_info.Find(id);
            if (user != null)
            {
                _dbContext.User_info.Remove(user);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }


        // GET: User/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
