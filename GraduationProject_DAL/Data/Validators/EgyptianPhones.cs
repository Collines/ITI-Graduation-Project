using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Data.Validators
{
    public class EgyptianPhones : ValidationAttribute
    {
        public override bool IsValid(object obj)
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
