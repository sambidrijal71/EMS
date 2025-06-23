using EmployeeManagementSystem.Domain.Exceptions;

namespace EmployeeManagementSystem.Domain.ValueObjects
{
    public class DateOfBirth
    {
        public DateOnly Value { get; }
        public int Age => DateTime.Now.Year - Value.Year;
        public DateOfBirth(DateOnly value)
        {
            var age = DateTime.Now.Year - value.Year;
            if (age < 18)
            {
                throw new InvalidDateOfBirthException();
            }
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not DateOfBirth other)
                return false;
            return Value.Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}