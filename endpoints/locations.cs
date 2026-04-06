using calendarium.API.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class LocationsEndpoints
{
    public static void MapLocationEndpoints(this IEndpointRouteBuilder app)
    {
        const string getLocationByIdName = "GetLocationById";

        var locations = new List<LocationDto>
        {
            new(1, "123 Main St", true),
            new(2, "456 Oak Ave", true),
            new(3, "789 Pine Rd", true)
        };



        // GET /locations
        app.MapGet("/locations", (AppDbContext db) =>
        {
            var locations = db.Locations.ToList();
            return Results.Ok(locations);
        });

        // GET /users/{id}
        app.MapGet("/locations/{id}", async (AppDbContext db, int id) =>
        {
            var location = await db.Locations.FindAsync(id);
            if (location == null)
                return Results.NotFound("Location not found.");
            return Results.Ok(location);
        }).WithName(getLocationByIdName);

        // POST /locations
        app.MapPost("/locations", async (AppDbContext db, CreateLocationDto newLocation) =>
        {
            var location = new Location
            {
                address = newLocation.address,
                active = true
            };

            db.Add(location);
            db.SaveChanges();

            return Results.CreatedAtRoute(getLocationByIdName, new { id = location.id }, location);
        });

        // PUT /locations/{id}
        app.MapPut("/locations/{id}", async (int id, UpdateLocationDto updatedLocation, AppDbContext db) =>
        {
            var location = await db.Locations.FindAsync(id);
            if (location == null)
                return Results.NotFound("Location not found.");
            location.address = updatedLocation.address;
            await db.SaveChangesAsync();
            return Results.Ok(location);
        });

        // PUT /locations/deactivate/{id}
        app.MapPut("/locations/deactivate/{id}", async (int id, AppDbContext db) =>
        {
            var location = await db.Locations.FindAsync(id);
            if (location == null)
                return Results.NotFound("Location not found.");
            if (!location.active)
                return Results.BadRequest("Location is already deactivated.");
            location.active = false;
            await db.SaveChangesAsync();
            return Results.Ok(location);
        });

        // PUT /locations/restore/{id}
        app.MapPut("/locations/restore/{id}", async (int id, AppDbContext db) =>
        {
            var location = await db.Locations.FindAsync(id);
            if (location == null)
                return Results.NotFound("Location not found.");
            if (location.active)
                return Results.BadRequest("Location is already active.");
            location.active = true;
            await db.SaveChangesAsync();
            return Results.Ok(location);
        });
              
        
    }
}