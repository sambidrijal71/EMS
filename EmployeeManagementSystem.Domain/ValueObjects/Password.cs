using System.Text.RegularExpressions;
using EmployeeManagementSystem.Domain.Exceptions;

namespace EmployeeManagementSystem.Domain.ValueObjects
{
    public class Password
    {
        public string Value { get; }
        public Password(string value)
        {
            var isValid = Regex.IsMatch(value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$");
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidPasswordException("Password is required.");
            }
            if (value.Length < 8)
            {
                throw new InvalidPasswordException("Password should be minimum 8 characters.");
            }
            if (!isValid)
            {
                throw new InvalidPasswordException("Password should have at least one lower, upper, number and special character from (?=.*[#$^+=!*()@%&] ).");
            }
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Password other)
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