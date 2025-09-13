namespace Unilink.Roulette.Api.Models
{
    public enum BetType { Color, ParidadDeColor, NumeroYColor }

    public record BetRequest(
        BetType Type,
        decimal Stake,
        string? Color,
        string? Paridad,
        int? Number,
        SpinResult Outcome
    );

    public record PayoutResponse(decimal Delta, bool Won);
}
