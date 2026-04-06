namespace calendarium.API.Dtos;
// A DTO (Data Transfer Object) is a simple object that is used to transfer data between layers of an application. It is often used to encapsulate data and to decouple the internal representation of data from the external representation. In this case, the AppointmentDto class is likely used to represent an appointment in the application, and it may contain properties such as Id, Name, Email, etc. However, since the class is currently empty, it does not contain any properties or methods.
public record AppointmentDto(
    int id,
    int clientId,
    int providerId,
    int locationId,
    DateOnly date,
    TimeOnly time,
    bool active
);

public record CreateAppointmentDto(
   int client_id,
    int provider_id,
    int location_id,
    DateOnly date,
    TimeOnly time
);

public record UpdateAppointmentDto(
  int client_id,
    int provider_id,
    int location_id,
    DateOnly date,
    TimeOnly time,
    bool active
);  
