using CodeFirstApproachDemo.Models;
using CodeFirstApproachDemo.Repository;
using CodeFirstApproachDemo.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstApproachDemo.Controllers;

[Authentication]
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

   
    public IActionResult DepartmentList()
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
            return RedirectToAction("DepartmentList");
        }
        return View();
    }

    [HttpGet]
    public IActionResult Update(int id) 
    { 
        var request = genericRepository.GetById(id);
        
        return View(request);
    }

    [HttpPost]
    public IActionResult Edit(Department request) 
    {
        genericRepository.Update(request);
        genericRepository.Save();
        return RedirectToAction("DepartmentList");
    }

    [HttpGet]
    public IActionResult DepartmentDetails(int id) 
    { 
        return View(genericRepository.GetById(id));
    }

    [HttpGet]
    public IActionResult Delete(int id) 
    {
        return View(genericRepository.GetById(id));
    }

    [HttpPost,ActionName("Delete")]
    public IActionResult DeleteConfirm(int id) 
    { 
        //var data = genericRepository.GetById(id);
        genericRepository.Delete(id);
        genericRepository.Save();
        return RedirectToAction("DepartmentList");
    }
}
