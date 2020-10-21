using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagment.Models
{
    public interface IStaffRepository
    {
        Staff GetStaff(int Id);
        IEnumerable<Staff> GetAllStaff();
        Staff Add(Staff staff);
        Staff Update(Staff staffChanges);
        Staff Delete(int id);
    }         //Functions for the staff
}
