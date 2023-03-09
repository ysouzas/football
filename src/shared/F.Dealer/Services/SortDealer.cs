using F.Dealer.Extensions;
using F.Dealer.Interfaces;
using F.Models;

namespace F.Dealer.Services;

public class SortDealer : IDealer
{
    public Func<List<Player>, Player> MethodToGetPlayer { get; set; } = null;
    public Func<Dictionary<int, Team>, int> MethodToGetTeamKey { get; set; } = null;


    public Func<List<Player>, Player> MethodToGetPlayer2 { get; set; } = null;
    public Func<Dictionary<int, Team>, int> MethodToGetTeamKey2 { get; set; } = null;

    public Dictionary<int, Team> SortTeams(List<Player> players, int numberOfTeams)
    {
        var dic = DictionaryExtensions.InitializeDictionary<int, Team>().Fillictionary(3);
        var count = players.Count;
        var isWeak = false;
        DecideMethod(isWeak);

        for (int i = 0; i < count; i++)
        {
            var key = this.MethodToGetTeamKey(dic);

            var player = this.MethodToGetPlayer(players);

            players.Remove(player);

            if (player != null)
                AddPlayerAndUpdateScore(dic, key, player);

            if (((i + 1) % 3) == 0)
            {
                isWeak = !isWeak;
                DecideMethod(isWeak);
            }

        }
        return dic;
    }

    public Dictionary<int, Team> SortTeamsRandom(List<Player> players, int numberOfTeams)
    {
        var dic = DictionaryExtensions.InitializeDictionary<int, Team>().Fillictionary(3);
        var numberOfPossibilities = 1000000;
        decimal bet = 0.10M;
        var totalScore = players.Sum(p => p.GeneralScore());

        var acceptableDifference = (totalScore % 3) == 0 ? 0.0M : 0.01M;

        for (int i = 0; i < numberOfPossibilities; i++)
        {
            var r = new Random();

            var randomTeams = players.OrderBy(i => r.Next()).Chunk(5).OrderBy(p => p.Sum(p => p.GeneralScore())).ToArray();
            var differenceFromTeam0 = randomTeams[0].Sum(p => p.GeneralScore());
            var differenceFromTeam1 = randomTeams[1].Sum(p => p.GeneralScore());
            var differenceFromTeam2 = randomTeams[2].Sum(p => p.GeneralScore());

            var differenceBetweenTeam2And0 = differenceFromTeam2 - differenceFromTeam0;

            if (differenceBetweenTeam2And0 < bet)
            {
                bet = differenceBetweenTeam2And0;
                var teams = randomTeams.Select(asa => new Team(asa.Sum(p => p.GeneralScore()), asa.ToList())).ToArray();


                for (int a = 0; a < teams.Length; a++)
                {
                    dic[a] = teams[a];
                }
            }

            if (bet == acceptableDifference || bet == 0.00M)
            {
                return dic;
            }
        }

        return dic;
    }

    private void AddPlayerAndUpdateScore(Dictionary<int, Team> dic, int key, Player player)
    {
        dic[key].AddPlayerAndUpdateScore(player);
    }

    private void DecideMethod(bool isWeak)
    {
        if (isWeak)
        {
            this.MethodToGetPlayer = (List<Player> players) => players.OrderBy(c => c.GeneralScore()).FirstOrDefault();
            this.MethodToGetTeamKey = (Dictionary<int, Team> dic) => dic.OrderBy(d => d.Value.Players.Count).ThenByDescending(d => d.Value.Players.Sum(v => v.GeneralScore())).FirstOrDefault().Key;
        }
        else
        {
            this.MethodToGetPlayer = (List<Player> players) => players.OrderByDescending(c => c.GeneralScore()).FirstOrDefault();
            this.MethodToGetTeamKey = (Dictionary<int, Team> dic) => dic.OrderBy(d => d.Value.Players.Count).ThenBy(d => d.Value.Players.Sum(v => v.GeneralScore())).FirstOrDefault().Key;
        }
    }
}

