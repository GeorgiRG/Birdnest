using Birdnest.Data;
using Birdnest.Dto;
using Birdnest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Birdnest.Controllers.API
{
    [Route("api/ViolationsData")]
    [ApiController]
    public class ViolationsDataController : ControllerBase
    {
        private readonly IViolationService _collectData;

        public ViolationsDataController(IViolationService collectData)
        {
            _collectData = collectData;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViolationDataDto>>> GetViolations()
        {
            return await _collectData.CollectViolData();
            
        }
    }
}
