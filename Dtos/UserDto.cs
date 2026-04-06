namespace calendarium.API.Dtos;
// A DTO (Data Transfer Object) is a simple object that is used to transfer data between layers of an application. It is often used to encapsulate data and to decouple the internal representation of data from the external representation. In this case, the UserDto class is likely used to represent a user in the application, and it may contain properties such as Id, Name, Email, etc. However, since the class is currently empty, it does not contain any properties or methods.
public record UserDto(
    int id,
    string name,
    string email,
    string password_hash,
    string role,
    bool active
);

public record CreateUserDto(
    string name,
    string email,
    string password_hash,
    string role
);

public record UpdateUserDto(
    string name,
    string email,
    string password_hash,
    string role,
    bool active
);  
