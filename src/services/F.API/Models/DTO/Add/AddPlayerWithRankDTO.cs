namespace F.API.Models.DTO.Add;

public record struct AddPlayerWithRankDTO
{
    public AddPlayerWithRankDTO(decimal score, DayOfWeek dayOfWeek, string name, DateTime date)
    {
        Score = score;
        DayOfWeek = dayOfWeek;
        Name = name;
        Date = date;
    }

    public decimal Score { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
}
