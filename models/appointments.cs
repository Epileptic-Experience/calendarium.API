using System.ComponentModel.DataAnnotations.Schema;

public class Appointment
{
    public int id { get; set; }
    public int client_id { get; set; }
    public int provider_id { get; set; }
    public int location_id { get; set; }
    public DateOnly date { get; set; } // Mapea a DATE
    [Column("time", TypeName = "timestamp without time zone")]
    public DateTime time { get; set; }
    public bool active { get; set; }

    public User? Client { get; set; }
    public User? Provider { get; set; }
    public Location? Location { get; set; }
}
