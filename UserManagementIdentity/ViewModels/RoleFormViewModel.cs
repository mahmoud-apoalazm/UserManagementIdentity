using System.ComponentModel.DataAnnotations;

namespace UserManagementIdentity.ViewModels
{
    public class RoleFormViewModel
    {
        [StringLength(256)]
        public string Name { get; set; }=string.Empty;
    }
}
