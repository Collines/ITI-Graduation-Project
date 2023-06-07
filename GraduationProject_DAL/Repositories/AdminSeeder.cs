using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Repositories
{
    public class AdminSeeder : ISeeder
    {
        private readonly HospitalDBContext context;
        private readonly IConfiguration configuration;

        public AdminSeeder(HospitalDBContext _context, IConfiguration _configuration)
        {
            context = _context;
            configuration = _configuration;
        }

        public async Task Seed()
        {
            Admin[] Admins = new Admin[] { 
                new Admin {
                    UserName = "admin1",
                    Password = "admin1"
                },
                new Admin {
                    UserName = "admin2",
                    Password = "admin2"
                },
                new Admin {
                    UserName = "admin3",
                    Password = "admin3"
                }
            };

            if (!context.Admins.Any())
            {
                // Create an instance of the Admin entity
                foreach(var admin in Admins)
                {
                    // Add the admin entity to the context
                    context.Admins.Add(admin);

                    // Save changes to the database
                    context.SaveChanges();
                }
                
            }
        }
    }
}
