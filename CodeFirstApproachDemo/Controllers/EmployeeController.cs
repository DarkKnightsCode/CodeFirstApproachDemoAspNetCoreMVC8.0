using CodeFirstApproachDemo.Models;
using CodeFirstApproachDemo.Models.ViewModel;
using CodeFirstApproachDemo.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CodeFirstApproachDemo.Controllers;
public class EmployeeController : Controller
{
    private readonly CompanyDbContext context;

    public EmployeeController(CompanyDbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// To display the list of employees
    /// </summary>
    /// <returns></returns>
    [Authentication]
    public IActionResult Index()
    {
        try
        {
            var Data = (from e in context.Employees
                        join d in context.Departments
                        on e.DepartmentId equals d.DepartmentId
                        select new EmployeeDepartmentViewModel
                        {
                            EmployeeId = e.EmployeeId,
                            First_Name = e.First_Name,
                            Last_Name = e.Last_Name,
                            Gender = e.Gender,
                            Department_Name = d.DepartmentName,
                            Department_Code = d.DepartmentCode,
                        }).ToList();

            return View(Data);
        }
        catch (Exception ex) 
        {
            // Log the exception
            // Return an error view or message
            return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }
    }

    /// <summary>
    /// To display the create employee page and load dropdown with deafault values 
    /// </summary>
    /// <returns></returns>
    [Authentication]
    public IActionResult Create() 
    {
        LoadDropDown();
        return View();
    }

    /// <summary>
    /// Create the new entry to save the data to database
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authentication]
    [HttpPost]
    public IActionResult Create(Employee request)
    {
        try { 
        if (request == null) 
        {
            return BadRequest();
        }
        context.Employees.Add(request);
        context.SaveChanges();
        TempData["CreateResponse"] = "Employee Added...";
        return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Log the exception
            // Return an error view or message
            return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }
    }

    /// <summary>
    /// To fetch the data for employee using id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authentication]
    [HttpGet("{id}")]
    public IActionResult Details(int id) 
    {
        try
        {
            var Data = (from e in context.Employees
                        join d in context.Departments
                        on e.DepartmentId equals d.DepartmentId
                        where e.EmployeeId == id
                        select new EmployeeDepartmentViewModel
                        {
                            EmployeeId = e.EmployeeId,
                            First_Name = e.First_Name,
                            Last_Name = e.Last_Name,
                            Gender = e.Gender,
                            Department_Name = d.DepartmentName,
                            Department_Code = d.DepartmentCode,
                        }).FirstOrDefault();
            if (Data == null)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
            }
            return View(Data);
        }
        catch (Exception ex)
        {
            // Log the exception
            // Return an error view or message
            return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }
    }


    /// <summary>
    /// to load the data for existing employee and show edit view  
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authentication]
    [HttpGet]
    public IActionResult Edit(int id) 
    {
        try
        {
            LoadDropDown();
            var Data = context.Employees.Find(id);
            if (Data == null)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
                //return NotFound();
            }
            return View(Data);
        }
        catch (Exception ex)
        {
            // Log the exception
            // Return an error view or message
            return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }
    }

    /// <summary>
    /// To update the existing employee data and save to database 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Edit(Employee request) 
    {
        try
        {
            if (request == null)
            {
                return BadRequest();
            }
            context.Employees.Update(request);
            context.SaveChanges();
            TempData["UpdateResponse"] = "Employee Data Updated...";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Log the exception
            // Return an error view or message
            return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }
    }

    /// <summary>
    /// fetch the information of existing employee and load delete view 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IActionResult Delete(int id)
    {
        try {
            var Data = (from e in context.Employees
                        join d in context.Departments
                        on e.DepartmentId equals d.DepartmentId
                        where e.EmployeeId == id
                        select new DeleteEmployeeModel
                        {
                            EmployeeId = e.EmployeeId,
                            First_Name = e.First_Name,
                            Last_Name = e.Last_Name,
                            Gender = e.Gender,
                            Department = d.DepartmentName
                        }).FirstOrDefault();
            return View(Data);
        }
        catch (Exception ex)
        {
            // Log the exception
            // Return an error view or message
            return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }
    }


    /// <summary>
    /// to delete the employee and update the database
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost,ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            var data = context.Employees.Find(id);
            context.Employees.Remove(data);
            context.SaveChanges();
            TempData["DeleteResponse"] = "Employee Data Removed...";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Log the exception
            // Return an error view or message
            return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }
    }
    /// <summary>
    /// To load the dropdown data  
    /// </summary>
    private void LoadDropDown() 
    {
        DepartmentListModel model = new DepartmentListModel();
        model.DepartmentList = new List<SelectListItem>();

        var data = context.Departments.ToList();
        foreach (var items in data)
        {
            model.DepartmentList.Add(new SelectListItem
            {
                Text = items.DepartmentName,
                Value = items.DepartmentId.ToString()
            });
        }
        ViewBag.DepartmentList = model.DepartmentList;
    }

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //public IActionResult Error()
    //{
    //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //}
}

