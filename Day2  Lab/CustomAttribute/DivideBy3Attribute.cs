using System.ComponentModel.DataAnnotations;

namespace Day2__Lab.CustomAttribute
{
    public class DivideBy3Attribute:ValidationAttribute
    {
        public DivideBy3Attribute()
        {
            ErrorMessage = "Value must be divisible by 3.";
        }
        public override bool IsValid(object? value)
        {
            var data = int.Parse(value.ToString());
            if (data % 3 == 0)
                return true;
            return false;
        }
    }
}
