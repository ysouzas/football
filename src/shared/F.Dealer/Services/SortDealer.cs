using F.Dealer.Extensions;
using F.Dealer.Interfaces;
using F.Models;

namespace F.Dealer.Services;

public class SortDealer : IDealer
{
    public Func<List<Player>, Player> MethodToGetPlayer { get; set; } = null;
    public Func<Dictionary<int, Team>, int> MethodToGetTeamKey { get; set; } = null;

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
