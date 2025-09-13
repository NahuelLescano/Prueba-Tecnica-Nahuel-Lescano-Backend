namespace Unilink.Roulette.Api.Models
{
    public record SpinResult(int Number, string Color)
    {
        public bool IsEven => Number % 2 == 0;
    }
}
