using System.ComponentModel;

namespace EmployeeManagementSystem.Domain.Enums
{
    public enum EmploymentStatus
    {

        [Description("Contract")]
        Contract,

        [Description("Full Time")]
        FullTime,

        [Description("Part Time")]
        PartTime,

        [Description("Probation")]
        Probation
    }
}