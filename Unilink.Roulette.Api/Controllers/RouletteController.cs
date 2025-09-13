using Microsoft.AspNetCore.Mvc;
using Unilink.Roulette.Api.Models;

namespace Unilink.Roulette.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RouletteController : ControllerBase
{
    private static readonly string[] Colors = ["rojo", "negro"];
    private readonly Random _rng = new();

    // 1) Devolver número 0..36 y color (rojo/negro)
    [HttpGet("spin")]
    public ActionResult<SpinResult> Spin()
    {
        int number = _rng.Next(0, 37);                 // 0..36
        string color = Colors[_rng.Next(0, Colors.Length)];
        return new SpinResult(number, color);
    }

    // 4) Devolver monto del premio según apuesta
    [HttpPost("payout")]
    public ActionResult<PayoutResponse> Payout([FromBody] BetRequest bet)
    {
        decimal stake = bet.Stake;
        bool won = false;
        decimal prize = 0m;

        // Normalizar entradas
        string? color = bet.Color?.Trim().ToLowerInvariant();
        string? paridad = bet.Paridad?.Trim().ToLowerInvariant();

        switch (bet.Type)
        {
            case BetType.Color:
                won = color is "rojo" or "negro" && bet.Outcome.Color == color;
                prize = won ? stake * 0.5m : 0m; // gana la mitad si acierta color
                break;

            case BetType.ParidadDeColor:
                bool parityOk = bet.Outcome.IsEven ? paridad == "par" : paridad == "impar";
                bool colorOk = color is "rojo" or "negro" && bet.Outcome.Color == color;
                won = parityOk && colorOk;
                prize = won ? stake * 1.0m : 0m; // gana lo apostado si acierta ambos
                break;

            case BetType.NumeroYColor:
                bool numberOk = bet.Number is >= 0 and <= 36 && bet.Outcome.Number == bet.Number;
                bool colorOk2 = color is "rojo" or "negro" && bet.Outcome.Color == color;
                won = numberOk && colorOk2;
                prize = won ? stake * 3.0m : 0m; // gana el triple si acierta ambos
                break;
        }

        // Si no acierta, pierde lo apostado
        decimal delta = won ? prize : (-stake);
        return new PayoutResponse(delta, won);
    }
}
