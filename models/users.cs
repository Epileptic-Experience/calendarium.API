public class User
{
    public int id { get; set; }
    public  string? name { get; set; }
    public  string? email { get; set; }
    public  string? password_hash { get; set; }
    public  string? role { get; set; }
    public  bool active { get; set; }

    public List<Appointment>? Appointments { get; set; }
}