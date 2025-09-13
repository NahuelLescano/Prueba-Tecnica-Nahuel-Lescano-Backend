using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unilink.Roulette.Api.Data;
using Unilink.Roulette.Api.models;

namespace Unilink.Roulette.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(AppDbContext db) : ControllerBase
{
    public record SaveRequest(string Name, decimal Delta);
    public record BalanceResponse(string Name, decimal Amount);

    // Obtener saldo por nombre (case-insensitive)
    [HttpGet("{name}")]
    public async Task<ActionResult<BalanceResponse>> Get(string name)
    {
        string key = name.Trim().ToUpperInvariant();
        var user = await db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Name.ToUpper() == key);

        return user is null
            ? new BalanceResponse(name, 0m)
            : new BalanceResponse(user.Name, user.Amount);
    }

    // 2) Guardar monto del usuario en BD
    // 3) Si existe, sumar/restar al saldo existente
    [HttpPost("save")]
    public async Task<ActionResult<BalanceResponse>> Save([FromBody] SaveRequest req)
    {
        string key = req.Name.Trim().ToUpperInvariant();
        var user = await db.Users.FirstOrDefaultAsync(u => u.Name.ToUpper() == key);

        if (user is null)
        {
            user = new UserBalance
            {
                Name = req.Name.Trim(),
                Amount = req.Delta
            };
            db.Users.Add(user);
        }
        else
        {
            user.Amount += req.Delta; // suma o resta
        }

        await db.SaveChangesAsync();
        return new BalanceResponse(user.Name, user.Amount);
    }
}

