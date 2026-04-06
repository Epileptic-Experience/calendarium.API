public class Location
{
    public int id { get; set; }
    public  string? address { get; set; }
    public  bool active { get; set; }

    public List<Appointment>? Appointments { get; set; }
}