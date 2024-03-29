﻿using Microsoft.AspNetCore.Http;
using StaffManagment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagment.ViewModels
{
    public class StaffCreateViewModel
    {
        [Required, MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public String Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid email format")]
        [Display(Name = "School Email")]
        public String Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        [Required]
        public Subj? Subjects { get; set; }
        [Required]
        public Occu? Occupation { get; set; }
        public IFormFile Photo { get; set; }
    }
}
