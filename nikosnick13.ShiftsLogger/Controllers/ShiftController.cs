using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nikosnick13.ShiftsLoggerAPI.Services;
using nikosnick13.ShiftsLoggerAPI.Model;

namespace nikosnick13.ShiftsLoggerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : ControllerBase
{
    private readonly IShiftServices _shiftService;

    public ShiftController(IShiftServices shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Shift>> GetAllShifts()
    {
        return Ok(_shiftService.GetAllShifts());
    }

    [HttpGet("{id}")]
    public ActionResult<Shift> GetShiftById(int id)
    {
        var savedShiftId = _shiftService.GetShiftById(id);

        if (savedShiftId == null) return NotFound($"Shift with ID {id} not found.");
        return Ok(savedShiftId);
    }

    [HttpPost]
    public ActionResult<Shift> CreateShift(Shift shift)
    {
        return Ok(_shiftService.CreateShift(shift));
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteShift(int id ) 
    {
        var shift = _shiftService.GetShiftById(id);

        if (shift == null) return NotFound($"Shift with ID {id} not found.");
        _shiftService.DeleteShift(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult<Shift> UpdateShift(int id, [FromBody] Shift shift)
    {
        if (id != shift.Id)
        {
            shift.Id = id;
        }

        var updatedShift = _shiftService.UpdateShift(shift);

        if (updatedShift == null)
        {
            return NotFound($"Shift with ID {id} not found.");
        }

        return Ok(updatedShift);
    }
}
