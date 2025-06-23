using System.Text.RegularExpressions;
using EmployeeManagementSystem.Domain.Exceptions;

namespace EmployeeManagementSystem.Domain.Validation
{
    public static class NameValidator
    {
        public static string ValidateName(string name, string nameDescription)
        {
            var isValidName = Regex.IsMatch(name, @"\b([A-ZÀ-ÿ][-,a-z. ']+[ ]*)+");
            if (String.IsNullOrEmpty(name))
            {
                throw new InvalidNameException(nameDescription, "cannot be empty.");
            }
            if (name.Length > 20)
            {
                throw new InvalidNameException(nameDescription, "should be less than 20 characters.");
            }
            if (!isValidName)
            {
                throw new InvalidNameException(nameDescription, $"{name} is invalid.");
            }
            return name;
        }
    }
}