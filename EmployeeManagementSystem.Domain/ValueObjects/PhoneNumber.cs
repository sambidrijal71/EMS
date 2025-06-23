using System.Text.RegularExpressions;
using EmployeeManagementSystem.Domain.Exceptions;

namespace EmployeeManagementSystem.Domain.ValueObjects
{
    public class PhoneNumber
    {
        public string Value { get; }

        public PhoneNumber(string value)
        {
            var isValid = Regex.IsMatch(value, @"^(?:\+?61|0)[2-478](?:[ -]?[0-9]){8}$");
            if (!isValid)
            {
                throw new InvalidPhoneNumberException(value);
            }
            Value = Regex.Replace(value, @"[ \-]", string.Empty);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not PhoneNumber other)
            {
                return false;
            }
            return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }
        public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();
    }
}