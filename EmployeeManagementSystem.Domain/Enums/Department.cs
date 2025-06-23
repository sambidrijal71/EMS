using System.ComponentModel;

namespace EmployeeManagementSystem.Domain.Enums
{
    public enum Department
    {
        [Description("Finance")]
        Finance,
        [Description("Marketing")]
        Marketing,
        [Description("Operations Management")]
        OperationsManagement,
        [Description("Human Resource")]
        HumanResource,
        [Description("Information Technology")]
        InformationTechnology,
        [Description("Legal Operations")]
        LegalOperations
    }
}