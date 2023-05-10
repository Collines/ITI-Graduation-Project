using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Data.Models
{
    public class EgyptianPhones : ValidationAttribute
    {
        public EgyptianPhones() { }

        public override bool IsValid(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                if (obj is string)
                {
                    //"^(?:\+20|20|0)?(10|11|12|15)\d{8}$"
                    Regex pattern = new Regex("^01[0125][0-9]{8}$");
                    string input = (string)obj;
                    if (pattern.IsMatch(input))
                    {
                        return true;
                    }
                    else
                    {
                        ErrorMessage = "Enter A valid Phone Number"; //user validation error
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
