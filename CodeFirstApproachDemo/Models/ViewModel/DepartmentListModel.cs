using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeFirstApproachDemo.Models.ViewModel;
public class DepartmentListModel
{
    public int Id { get; set; }
    public List<SelectListItem> DepartmentList { get; set; }
}
