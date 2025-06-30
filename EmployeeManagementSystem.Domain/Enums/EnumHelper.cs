using System.ComponentModel;

namespace EmployeeManagementSystem.Domain.Enums
{
    public static class EnumHelper
    {
        public record EnumDto(int Value, string Label);
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                  .FirstOrDefault() as DescriptionAttribute;

            return attribute?.Description ?? value.ToString();
        }

        public static List<EnumDto> ToList<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                       .Cast<TEnum>()
                       .Select(e => new EnumDto(
                           Convert.ToInt32(e),
                           e.GetDescription()
                       ))
                       .ToList();
        }
    }
}