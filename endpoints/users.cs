using calendarium.API.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        const string getUserByIdName = "GetUserById";

        var users = new List<UserDto>
        {
            new(1, "John Doe", "john.doe@example.com", "hashed_password", "user", true),
            new(2, "Jane Smith", "jane.smith@example.com", "hashed_password", "admin", true),
            new(3, "Bob Johnson", "bob.johnson@example.com", "hashed_password", "provider", true)
        };


        app.MapGet("/debug-db", async (AppDbContext db) =>
        {
            var conn = db.Database.GetDbConnection();
            return Results.Ok(new {
                Database = conn.Database,
                DataSource = conn.DataSource
            });
        });
        // GET /users
        app.MapGet("/users", (AppDbContext db) =>
        {
            var users = db.Users.ToList();
            return Results.Ok(users);
        });
        // GET /users/{id}
        app.MapGet("/users/{id}", async (AppDbContext db, int id) =>
        {
            var user = await db.Users.FindAsync(id);
            return user is not null ? Results.Ok(user) : Results.NotFound();
        }).WithName(getUserByIdName);

        // POST /users
        app.MapPost("/users", async ( AppDbContext db,CreateUserDto newUser) =>
        {
             var user = new User
            {
                name = newUser.name,
                email = newUser.email,
                password_hash = newUser.password_hash,  
                role = newUser.role,
                active = true
            };

            db.Add(user);
            await db.SaveChangesAsync();

            return Results.CreatedAtRoute(getUserByIdName, new { id = user.id }, user);
        });

        // PUT /users/{id}
        app.MapPut("/users/{id}", async (int id, UpdateUserDto updatedUser, AppDbContext db) =>
        {
            var user = await db.Users.FindAsync(id);

            if (user == null)
                return Results.NotFound();

            user.name = updatedUser.name;
            user.email = updatedUser.email; 
            user.role = updatedUser.role;
            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        // PUT /users/deactivate/{id}
        app.MapPut("/users/deactivate/{id}", async (AppDbContext db, int id) =>
        {
            var user = await db.Users.FindAsync(id);

            if (user == null)
                return Results.NotFound();


            if (!user.active)
                return Results.BadRequest("User is already deactivated.");

            user.active = false;
            await db.SaveChangesAsync();

            return Results.Ok(user);
        });

        // PUT /users/restore/{id}
        app.MapPut("/users/restore/{id}", async (AppDbContext db, int id) =>
        {
            var user = await db.Users.FindAsync(id);

            if (user == null)
                return Results.NotFound();

            if (user.active)
                return Results.BadRequest("User is already active.");

            user.active = true;
            await db.SaveChangesAsync();

            return Results.Ok(user);
        });
    }
}