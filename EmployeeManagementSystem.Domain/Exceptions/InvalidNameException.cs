namespace EmployeeManagementSystem.Domain.Exceptions
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException(string value, string message) : base($"{value} {message}")
        {

        }
    }
}