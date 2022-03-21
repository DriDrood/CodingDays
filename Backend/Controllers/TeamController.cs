using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingDays.Database;
using CodingDays.Database.Entities;
using CodingDays.Models.Dto.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingDays.Controllers;
public class TeamController : ControllerBase
{
    public TeamController(DB db)
    {
        _db = db;
    }

    private DB _db;

    public async Task<GetResp> Get()
    {
        Team[] teams = await _db.Teams
            .Include(t => t.Registrations)
            .ToArrayAsync();
        
        Dictionary<Guid, GetRespTeam> response = teams.ToDictionary(
            t => t.Id, 
            t => new GetRespTeam(
                t.Name, 
                t.CurrentStep == ESteps.Done, 
                t.CurrentStep == ESteps.Done
                    ? t.Registrations.Select(r => $"{r.Name} {r.Surname}").ToArray()
                    : new string[0]));

        return new GetResp(response);
    }
}
