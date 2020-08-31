using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagment.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staff>().HasData(
                    new Staff
                    {
                        Id = 1,
                        Name = "Aziz",
                        Email = "ac98844@avcol.school.nz",
                        Department = Dept.Technology,
                        Subjects = Subj.Computing,
                        Occupation = Occu.Teacher
                    },
                new Staff
                {
                    Id = 2,
                    Name = "Cooper",
                    Email = "ac98741@avcol.school.nz",
                    Department = Dept.Maths,
                    Subjects = Subj.Calculus,
                    Occupation = Occu.Teacher
                }
                );
        }
    }
}
