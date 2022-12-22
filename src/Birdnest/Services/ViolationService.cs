using Birdnest.Data;
using Birdnest.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Birdnest.Services
{
    public interface IViolationService
    {
        public Task<ActionResult<IEnumerable<ViolationDataDto>>> CollectViolData();
    }

    public class CollectData : IViolationService
    {
        private readonly BirdnestContext _context;
        public CollectData(BirdnestContext context)
        { 
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<ViolationDataDto>>> CollectViolData()
        {
            //as my local time doesn't work
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
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
                                            Time = TimeZoneInfo.ConvertTimeFromUtc(v.Time, zone).ToString("dd/MM/yyyy HH':'mm':'ss"),

                                            Duration = v.Duration

                                        }).OrderBy(dto => dto.Distance).ToListAsync();
            return dataDTO;
        }
    }
}
