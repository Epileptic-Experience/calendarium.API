using calendarium.API.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class appointmentsEndpoints
{
    public static void MapAppointmentEndpoints(this IEndpointRouteBuilder app)
    {
        const string getappointmentByIdName = "GetappointmentById";

        var Appointments = new List<AppointmentDto>
        {
            new AppointmentDto(1,1,1,1, DateOnly.FromDateTime(DateTime.Today), TimeOnly.FromDateTime(DateTime.Now), true),
            new AppointmentDto(2,1,1,1, DateOnly.FromDateTime(DateTime.Today), TimeOnly.FromDateTime(DateTime.Now), true),
            new AppointmentDto(3,1,1,1, DateOnly.FromDateTime(DateTime.Today), TimeOnly.FromDateTime(DateTime.Now), true)
        };

        app.MapGet("/debug-db", async (AppDbContext db) =>
        {
            var conn = db.Database.GetDbConnection();
            return Results.Ok(new
            {
                Database = conn.Database,
                DataSource = conn.DataSource
            });
        });


        // GET /appointments
        app.MapGet("/appointments", (AppDbContext db) =>
        {
            var appointments = db.Appointments.ToList();
            return Results.Ok(appointments);
        });

        // GET /users/{id}
        app.MapGet("/appointments/{id}", async (AppDbContext db, int id) =>
        {
            var appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
                return Results.NotFound("Appointment no encontrado.");
            return Results.Ok(appointment);

        }).WithName(getappointmentByIdName);

        // POST /appointments
        app.MapPost("/appointments", async (AppDbContext db, CreateAppointmentDto newappointment) =>
        {
            DateTime combinedDateTime = newappointment.date.ToDateTime(newappointment.time);
            var appointment = new Appointment
            {
                client_id = newappointment.client_id,
                provider_id = newappointment.provider_id,
                location_id = newappointment.location_id,
                date = newappointment.date,      // Se guarda en la columna DATE
                time = combinedDateTime, // Se guarda en la columna TIMESTAMP
                active = true
            };

            db.Add(appointment);
            db.SaveChanges();

            // Appointments.Add(appointment);

            return Results.CreatedAtRoute(getappointmentByIdName, new { id = appointment.id }, appointment);
        });

        // PUT /appointments/{id}
        app.MapPut("/appointments/{id}",async (int id, UpdateAppointmentDto updatedappointment, AppDbContext db) =>
        {
            var appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
                return Results.NotFound();
            DateTime combinedDateTime = updatedappointment.date.ToDateTime(updatedappointment.time);
            appointment.client_id = updatedappointment.client_id;
            appointment.provider_id = updatedappointment.provider_id;
            appointment.location_id = updatedappointment.location_id;
            appointment.date = updatedappointment.date;      // Se actualiza la columna DATE
            appointment.time = combinedDateTime; // Se actualiza la columna TIMESTAMP

            await db.SaveChangesAsync();


            return Results.NoContent();
        });

        // PUT /appointments/deactivate/{id}
        app.MapPut("/appointments/deactivate/{id}", async (int id,  AppDbContext db) =>
        {
            var appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
                return Results.NotFound();
            if (!appointment.active)
                return Results.BadRequest("appointment is already inactive.");
            appointment.active = false; 
            await db.SaveChangesAsync();


            return Results.NoContent();
        });

        // PUT /appointments/restore/{id}
        app.MapPut("/appointments/restore/{id}", async (int id,  AppDbContext db) =>
        {
            var appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
                return Results.NotFound();
            if (appointment.active)
                return Results.BadRequest("appointment is already active.");
            appointment.active = true; 
            await db.SaveChangesAsync();


            return Results.NoContent();
        });
    }
}