namespace F.API.Models.DTO.Add;

public record struct AddRanksDTO
{
    public AddRankDTO[] RanksDTO { get; set; }
}

