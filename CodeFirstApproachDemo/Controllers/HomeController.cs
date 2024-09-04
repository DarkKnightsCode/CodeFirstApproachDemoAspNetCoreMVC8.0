using CodeFirstApproachDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeFirstApproachDemo.Controllers;

public class HomeController : Controller
{
    private readonly CompanyDbContext context;

    public HomeController(CompanyDbContext context)
    {
        this.context = context;
    }
    // Get Action
    public IActionResult Login()
    {
        if (HttpContext.Session.GetString("UserName") == null)
        {
            return View();
        }
        else
        {
            return RedirectToAction("Index","Employee");
        }
    }
    //Post Action
    [HttpPost]
    public ActionResult Login(UserCredential request)
    {
        if (HttpContext.Session.GetString("UserName") == null)
        {
            if (ModelState.IsValid)
            {
               
                    var obj = context.userCredentials.Where(a => a.UserName.Equals(request.UserName) && a.Password.Equals(request.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        HttpContext.Session.SetString("UserName", obj.UserName.ToString());
                        return RedirectToAction("Index", "Employee");
                }
               
            }
        }
        else
        {
            return RedirectToAction("Login");
        }
        return View();
    }

    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("UserName");
        return RedirectToAction("Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
