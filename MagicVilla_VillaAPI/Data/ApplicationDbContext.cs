using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
	{
		
	}

	public DbSet<Villa> Villas { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Villa>().HasData(
				new Villa()
				{
					Id = 1,
					Name = "Royal Villa",
					Details = "Temp",
					Rate = 200,
					Occupancy = 5,
					ImageUrl = "https://www.google.ru/url?sa=i&url=https%3A%2F%2Fwww.booking.com%2Fhotel%2Fgr%2Felysian-villa.ru.html&psig=AOvVaw3Lew8r_MG419GJJcCC45I1&ust=1675271534608000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCMC97vem8vwCFQAAAAAdAAAAABAE",
					Sqrt= 500,
					Amenity = "",
					CreatedTime= DateTime.Now,
					UpdatedTime= DateTime.Now,
				}
				);
	}
}