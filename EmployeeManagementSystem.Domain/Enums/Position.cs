namespace EmployeeManagementSystem.Domain.Enums
{
    using System.ComponentModel;

    public enum Position
    {
        [Description("Chief Executive Officer")]
        Ceo,
        [Description("Chief Operating Officer")]
        Coo,
        [Description("Chief Technology Officer")]
        Cto,
        [Description("Account Director")]
        AccountDirector,
        [Description("Development Manager")]
        DevelopmentManager,
        [Description("Test Manager")]
        TestManager,
        [Description("ScrumMaster")]
        ScrumMaster,
        [Description("LeadDeveloper")]
        LeadDeveloper,
        [Description("LeadTestEngineer")]
        LeadTestEngineer,
        [Description("TestingManager")]
        TestingManager,
        [Description("SeniorDeveloper")]
        SeniorDeveloper,
        [Description("SeniorTestEngineer")]
        SeniorTestEngineer,
        [Description("AnalystDeveloper")]
        AnalystDeveloper,
        [Description("AnalystTester")]
        AnalystTester
    }
}