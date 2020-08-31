using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagment.ViewModels
{
    public class StaffEditViewModel : StaffCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
