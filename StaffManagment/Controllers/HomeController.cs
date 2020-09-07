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

        //[HttpPost]
        //[Route("[action]")]
        //public IActionResult Edit(StaffEditViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Staff staff = _staffRepository.GetStaff(model.Id);
        //        staff.Name = model.Name;
        //        staff.Email = model.Email;
        //        staff.Department = model.Department;
        //        staff.Subjects = model.Subjects;
        //        staff.Occupation = model.Occupation;



        //        if (model.Photo != null)
        //        {
        //            if (model.ExistingPhotoPath != null)
        //            {
        //                string filePath = Path.Combine(webHostEnvironment.WebRootPath,
        //                "images", model.ExistingPhotoPath);
        //                System.IO.File.Delete(filePath);
        //            }
        //                staff.PhotoPath = ProcessUploadedFile(model);

        //        }

        //        Staff updatedStaff = _staffRepository.Update(staff);
        //        return RedirectToAction("index");
        //    }

        //    return View(model);
        //}
        //+model.Photo.FileName;

        // Through model binding, the action method parameter
        // EmployeeEditViewModel receives the posted edit form data
        [HttpPost]
        [Route("[action]")]

        public IActionResult Edit(StaffEditViewModel model)
        {
            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the employee being edited from the database
                Staff staff = _staffRepository.GetStaff(model.Id);
                // Update the employee object with the data in the model object
                staff.Name = model.Name;
                staff.Email = model.Email;
                staff.Department = model.Department;
                staff.Subjects = model.Subjects;
                staff.Occupation = model.Occupation;

                // If the user wants to change the photo, a new photo will be
                // uploaded and the Photo property on the model object receives
                // the uploaded photo. If the Photo property is null, user did
                // not upload a new photo and keeps his existing photo
                if (model.Photo != null)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the employee object which will be
                    // eventually saved in the database
                    staff.PhotoPath = ProcessUploadedFile(model);
                }

                // Call update method on the repository service passing it the
                // employee object to update the data in the database table
                Staff updatedStaff = _staffRepository.Update(staff);

                return RedirectToAction("index");
            }

            return View(model);
        }


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



