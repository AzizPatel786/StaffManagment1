using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StaffManagment.Models;
using StaffManagment.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagment.Controllers
{

    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IStaffRepository _staffRepository;

        private readonly IWebHostEnvironment webHostEnvironment;


        public HomeController(IStaffRepository staffRepository,
                                 IWebHostEnvironment webHostEnvironment)
        {
            _staffRepository = staffRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Route("")]
        [Route("[action]")]
        [Route("/")]

        public ViewResult Index()
        {
            var model = _staffRepository.GetAllStaff();
            return View(model);
        }

        [Route("[action]/{id?}")]
        public ViewResult Details(int? id)
        {
                        
            Staff staff = _staffRepository.GetStaff(id.Value);

            if (staff == null)
            {
                Response.StatusCode = 404;
                return View("StaffNotFound", id.Value);
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Staff = staff,
                PageTitle = "Staff Details"
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]

        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [Route("[action]/{id}")]
        public ViewResult Edit(int id)
        {
            Staff staff = _staffRepository.GetStaff(id);
            StaffEditViewModel staffEditViewModel = new StaffEditViewModel
            {
                Id = staff.Id,
                Name = staff.Name,
                Email = staff.Email,
                Department = staff.Department,
                Subjects = staff.Subjects,
                Occupation = staff.Occupation,
                ExistingPhotoPath = staff.PhotoPath
            };
            return View(staffEditViewModel);
        }

        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    _staffRepository.Delete(id);
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [Route("[action]")]
        public IActionResult Edit(StaffEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Staff staff = _staffRepository.GetStaff(model.Id);
                staff.Name = model.Name;
                staff.Email = model.Email;
                staff.Department = model.Department;
                staff.Subjects = model.Subjects;
                staff.Occupation = model.Occupation;



                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                        "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                        staff.PhotoPath = ProcessUploadedFile(model);
                    
                }

                Staff updatedStaff = _staffRepository.Update(staff);
                return RedirectToAction("index");
            }

            return View(model);
        }
        //+model.Photo.FileName;

        private string ProcessUploadedFile(StaffCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {

                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.Photo.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(StaffCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                Staff newStaff = new Staff
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    Subjects = model.Subjects,
                    Occupation = model.Occupation,
                    PhotoPath = uniqueFileName
                };

                _staffRepository.Add(newStaff);
                return RedirectToAction("details", new { id = newStaff.Id });
            }

            return View();
        }
    }
}
