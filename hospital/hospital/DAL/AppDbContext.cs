using hospital.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hospital.DAL
{
	public class AppDbContext:IdentityDbContext<AppUser>
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-QOKRM5K;Database=hsm; integrated security=true");
        }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{

		}

        public AppDbContext()
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<BloodGroup> BloodGroups { get; set; }
		
		public DbSet<Department> Departments { get; set; }
		public DbSet<RoomMaster> RoomMasters { get; set; }
		public DbSet<Floor> Floors { get; set; }
		public DbSet<RoomCategory> RoomCategories { get; set; }
		public DbSet<RoomBed> RoomBeds { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<AppDetail> AppDetails { get; set; }
		public DbSet<OPD> OPDs { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<Inventory> Inventories { get; set; }
		public DbSet<ItemCategory> ItemCategories { get; set; }
		public DbSet<ItemStore> ItemStores { get; set; }
		public DbSet<IssueItem> IssueItems { get; set; }
		public DbSet<Product> Products { get; set; }
		
	}
}
