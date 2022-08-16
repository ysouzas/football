namespace F.Models
{
    public class Team
    {
        public decimal Score { get; set; }
        public List<Player> Players { get; set; }


        public Team()
        {
            Players = new List<Player>();   
        }

        public Team(decimal score, List<Player> players)
        {
            Score = score;
            Players = players;
        }
    }
}
