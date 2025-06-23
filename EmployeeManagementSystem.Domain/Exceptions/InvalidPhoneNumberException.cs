namespace EmployeeManagementSystem.Domain.Exceptions
{
    public class InvalidPhoneNumberException : Exception
    {
        public InvalidPhoneNumberException(string value) : base($"{value} is not a valid Phone Number") { }
    }
}