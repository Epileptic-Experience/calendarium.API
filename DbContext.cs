using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using EFCore.NamingConventions;


//aca tengo que setear el DbContext, que es la clase que representa la base de datos en mi aplicacion,
//y que me permite hacer consultas a la base de datos usando LINQ y otras herramientas de EF Core.
//En esta clase tengo que definir las tablas que voy a usar en mi aplicacion, y las relaciones entre ellas.
//Tambien tengo que configurar el DbContext para que use la cadena de conexion que le paso desde Program.cs, 
//y para que use las convenciones de nombrado que quiero usar en mi base de datos (en este caso, snake_case).
public class AppDbContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Location> Locations { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // representa las tablas y las relaciona con un tipo de modelo c#
        // luego tambien setea las relaciones entre las tablas
        //appointments
        modelBuilder.Entity<Appointment>().ToTable("appointments", "public");
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Client)
            .WithMany()
            .HasForeignKey(a => a.client_id);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Provider)
            .WithMany()
            .HasForeignKey(a => a.provider_id);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Location)
            .WithMany()
            .HasForeignKey(a => a.location_id);

        //users
        modelBuilder.Entity<User>().ToTable("users", "public")
        .HasMany(u => u.Appointments)
        .WithOne(a => a.Client)
        .HasForeignKey(a => a.client_id);


        //locations
        modelBuilder.Entity<Location>().ToTable("locations", "public");
        modelBuilder.Entity<Location>()
            .HasMany(l => l.Appointments)
            .WithOne(a => a.Location)
            .HasForeignKey(a => a.location_id);
    }
}

