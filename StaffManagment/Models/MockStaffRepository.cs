using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagment.Models
{
    public class MockStaffRepository : IStaffRepository
    {
        private List<Staff> _staffList;

        public MockStaffRepository()
        {
            _staffList = new List<Staff>()
            {
                new Staff() { Id = 1, Name = "Cooper", Department = Dept.Technology, Email = "ac99741@avcol.school.nz", Occupation = Occu.Teacher, Subjects = Subj.Computing},
                new Staff() { Id = 2, Name = "Aziz", Department = Dept.Maths, Email = "ac98844@avcol.school.nz", Occupation = Occu.Teacher, Subjects = Subj.Calculus},
                new Staff() { Id = 3, Name = "Malhar", Department = Dept.Science, Email = "ac98824@avcol.school.nz", Occupation = Occu.Teacher, Subjects = Subj.Physics},

            };
        }

        public Staff Add(Staff staff)
        {
           staff.Id = _staffList.Max(e => e.Id) + 1;
            _staffList.Add(staff);
            return staff;
        }

        public Staff Delete(int id)
        {
            Staff staff = _staffList.FirstOrDefault(e => e.Id == id);
            if (staff != null)
            {
                _staffList.Remove(staff);
            }
            return staff;
        }

        public IEnumerable<Staff> GetAllStaff()
        {
            return _staffList;
        }

        public Staff GetStaff(int Id)
        {
            return _staffList.FirstOrDefault(e => e.Id == Id);
                }

        public Staff Update(Staff staffChanges)
        {
            Staff staff = _staffList.FirstOrDefault(e => e.Id == staffChanges.Id);
            if (staff != null)
            {
                staff.Name = staffChanges.Name;
                staff.Email = staffChanges.Email;
                staff.Department = staffChanges.Department;
                staff.Subjects = staffChanges.Subjects;
                staff.Occupation = staffChanges.Occupation;

            }
            return staff;
        }
    }
}
