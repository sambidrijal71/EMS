using EmployeeManagementSystem.Domain.Validation;

namespace EmployeeManagementSystem.Domain.ValueObjects
{
    public class Name
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Name(string firstName, string lastName)
        {

            FirstName = NameValidator.ValidateName(firstName, "First Name");
            LastName = NameValidator.ValidateName(lastName, "Last Name");
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Name other)
            {
                return false;
            }
            return FirstName.Equals(other.FirstName, StringComparison.OrdinalIgnoreCase) && LastName.Equals(other.LastName, StringComparison.OrdinalIgnoreCase);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(
            FirstName.ToLowerInvariant(),
            LastName.ToLowerInvariant()
            );
        }
    }
}