using Birdnest.Data;
using Birdnest.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdnest.Controllers.API
{
    [Route("api/ViolationsData")]
    [ApiController]
    public class ViolationsDataController : ControllerBase
    {
        private readonly BirdnestContext _context;

        public ViolationsDataController(BirdnestContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViolationDataDto>>> GetTodoItems()
        {
            List<ViolationDataDto> dataDTO = await _context.Pilots
                                    .Join(_context.Violations, p => p.PilotID, v => v.PilotID, (p, v) =>
                                        new ViolationDataDto
                                        {
                                            PilotID = p.PilotID,
                                            FirstName = p.FirstName,
                                            LastName = p.LastName,
                                            PhoneNumber = p.PhoneNumber,
                                            Email = p.Email,
                                            Distance = v.Distance,
                                            ViolationLocationX = v.ViolationLocationX,
                                            ViolationLocationY = v.ViolationLocationY,
                                            Time = v.Time.ToLocalTime().ToString("dd/MM/yyyy HH':'mm':'ss"),
                                            Duration = v.Duration

                                        }).OrderBy(dto => dto.Distance).ToListAsync();
            return dataDTO;
        }
    }
}
