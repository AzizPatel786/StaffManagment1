using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagment.Models
{
    public class SQLStaffRepository : IStaffRepository
    {
        private readonly AppDbContext context;

        public SQLStaffRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Staff Add(Staff staff)
        {
            context.Staffs.Add(staff);
            context.SaveChanges();
            return staff;
        }

        public Staff Delete(int id)
        {
            Staff staff = context.Staffs.Find(id);
            if (staff != null)
            {
                context.Staffs.Remove(staff);
                context.SaveChanges();
            }
            return staff;
        }

        public IEnumerable<Staff> GetAllStaff()
        {
            return context.Staffs;
        }

        public Staff GetStaff(int Id)
        {
            return context.Staffs.Find(Id);
        }

        public Staff Update(Staff staffChanges)
        {
            var employee = context.Staffs.Attach(staffChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return staffChanges;
        }
    }
}
