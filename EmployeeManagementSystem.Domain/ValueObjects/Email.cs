using System.Text.RegularExpressions;
using EmployeeManagementSystem.Domain.Exceptions;

namespace EmployeeManagementSystem.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; }
        public Email(string value)
        {
            var isValid = Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!isValid) throw new InvalidEmailException(value);
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Email other)
                return false;

            return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);

        }
        public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();
    }
}