using Spectre.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nikosnick13.ShiftsLoggerUI.Models;

namespace nikosnick13.ShiftsLoggerUI;

public class TableVisualisation
{

    public static void ShowTable(IEnumerable<Shift> shifts) {

        var table = new Table();

        table.AddColumn("[yellow]Id[/]");
        table.AddColumn("[yellow]Name[/]");
        table.AddColumn("[yellow]Last Name[/]");
        table.AddColumn("[yellow]StartTime[/]");
        table.AddColumn("[yellow]EndTime[/]");
        table.AddColumn("[yellow]Duration [/]");


        foreach (var shift in shifts)
        {
            table.AddRow(
                shift.Id.ToString(),
                shift.WorkerName ?? "",
                shift.WorkerLastName ?? "",
                shift.StartTime.ToString("g"),
                shift.EndTime.ToString("g"),
                shift.Duration.ToString("0.##")
                );
        }

         AnsiConsole.Write(table);
 
         
    }

    internal static void ShowOneTable(Shift shift)
    {
        var table = new Table();

        table.AddColumn("[yellow]Id[/]");
        table.AddColumn("[yellow]Name[/]");
        table.AddColumn("[yellow]Last Name[/]");
        table.AddColumn("[yellow]StartTime[/]");
        table.AddColumn("[yellow]EndTime[/]");
        table.AddColumn("[yellow]Duration [/]");

        table.AddRow(
            shift.Id.ToString(),
            shift.WorkerName ?? "",
            shift.WorkerLastName ?? "",
            shift.StartTime.ToString("g"),
            shift.EndTime.ToString("g"),
            shift.Duration.ToString("0.##")
            );

        AnsiConsole.Write(table);
    }
}
