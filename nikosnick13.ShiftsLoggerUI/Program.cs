using nikosnick13.ShiftsLoggerUI.Services;
using nikosnick13.ShiftsLoggerUI;
namespace nikosnick13.ShiftsLoggerUI;

internal class Program
{
    static async Task Main(string[] args)
    { 
         
       
        var httpClient = new HttpClient();
        var shiftServices = new ShiftServices(httpClient);

        // ⬇️ Περνάμε το shiftServices στο UserInterface
        var ui = new UserInterface(shiftServices);

        await ui.MainMenu();
        
    }
}
