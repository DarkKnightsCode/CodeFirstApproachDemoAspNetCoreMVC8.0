using CodeFirstApproachDemo.Models;
using CodeFirstApproachDemo.Repository;
using CodeFirstApproachDemo.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstApproachDemo.Controllers;


public class DepartmentController : Controller
{
    private readonly IDepartmentRepository departmentRepo;
    private readonly IGenericRepository<Department> genericRepository;

    public DepartmentController
    (
        IDepartmentRepository departmentRepo,
        IGenericRepository<Department> genericRepository
    )
    {
        this.departmentRepo = departmentRepo;
        this.genericRepository = genericRepository;
    }

   
    public IActionResult Index()
    {
        List<Department> DepartmentsList = genericRepository.GetAll().ToList();
        return View(DepartmentsList);
    }
    [HttpGet]
    public IActionResult GetDepartmentByCode(string code)
    {
        List<Department> DeptByCodeList = departmentRepo.GetByDeptCode(code).ToList();
        return View(DeptByCodeList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Department request) 
    {
        if (ModelState.IsValid) 
        { 
            genericRepository.Insert(request);
            genericRepository.Save();
            return RedirectToAction("Index", "Employee");
        }
        return View();
    }

}
