namespace Unilink.Roulette.Api.models
{
    public class UserBalance
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Amount { get; set; }
    }
}
