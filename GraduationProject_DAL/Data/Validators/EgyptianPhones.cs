using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GraduationProject_DAL.Data.Validators
{
    public class EgyptianPhones : ValidationAttribute
    {
        public override bool IsValid(object? obj)
        {
            if (obj is string phone && !phone.IsNullOrEmpty())
            {
                Regex pattern = new Regex("^01[0125][0-9]{8}$");
                return pattern.IsMatch(phone);
            }
            else
            {
                ErrorMessage = "Enter A valid Phone Number"; //user validation error
                return false;
            }
        }
    }
}
