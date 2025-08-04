using System;
using System.Linq;

namespace nikosnick13.ShiftsLoggerUI.Models;

public class Shift
{
    public int Id { get; set; }
    public string? WorkerName { get; set; }
    public string? WorkerLastName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    // Υπολογίζει αυτόματα τη διάρκεια
    public double Duration => (EndTime - StartTime).TotalHours;

}
