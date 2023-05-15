using GraduationProject_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Interfaces.Authentication
{
	public interface IJWTManagerRepository
	{
		Token? Authenticate(Patient patient);
	}
}
