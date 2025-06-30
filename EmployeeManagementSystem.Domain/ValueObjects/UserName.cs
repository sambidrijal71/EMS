using System.Text.RegularExpressions;
using EmployeeManagementSystem.Domain.Exceptions;

namespace EmployeeManagementSystem.Domain.ValueObjects
{
    public class UserName
    {
        public string Value { get; }
        public UserName(string value)
        {
            var isValid = Regex.IsMatch(value, @"^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{0,19}$");
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidUsernameException("Username is required.");
            }
            if (value.Length > 20)
            {
                throw new InvalidUsernameException("Username should be below 20 characters.");
            }
            if (!isValid)
            {
                throw new InvalidUsernameException("Username should contain characters a-z, 0-9, underscores and periods.");
            }
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not UserName other)
            {
                return false;
            }
            return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Value.ToLowerInvariant().GetHashCode();
        }
    }
}