namespace EmployeeManagementSystem.Domain.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string value) : base($"{value} is not a valid email address.") { }
    }
}