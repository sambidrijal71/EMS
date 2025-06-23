namespace EmployeeManagementSystem.Domain.Exceptions
{
    public class InvalidDateOfBirthException : Exception
    {
        public InvalidDateOfBirthException() : base("Age should be greater than or equal to 18.") { }
    }
}