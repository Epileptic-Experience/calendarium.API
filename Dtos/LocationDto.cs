public record LocationDto(
    int id,
    string address,
    bool active
);

public record CreateLocationDto(
    string address
);

public record UpdateLocationDto(
    string address
);