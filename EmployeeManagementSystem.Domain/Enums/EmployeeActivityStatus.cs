using System.ComponentModel;

namespace EmployeeManagementSystem.Domain.Enums
{
    public enum EmployeeActivityStatus
    {
        [Description("Active")]
        Active,
        [Description("Inactive")]
        Inactive
    }
}