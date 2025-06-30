namespace EmployeeManagementSystem.Application.Exceptions
{
    public sealed class UserAuthenticationException : Exception
    {
        public UserAuthenticationException() { }
        public UserAuthenticationException(string message) : base(message) { }
        public UserAuthenticationException(string message, Exception innerException) : base(message, innerException) { }
    }
}