namespace F.API.Models.DTO.Model
{
    public record struct TeamDTO
    {
        public TeamDTO(PlayerDTO[] teams, decimal score)
        {
            Teams = teams;
            Score = score;
        }

        public PlayerDTO[] Teams { get; set; }
        public decimal Score { get; set; }

    }
}
