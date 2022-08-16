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
        //var value = players.Sum(p => p.Score())/numberOfTeams;
        //DecideMethod2(isWeak);

        for (int i = 0; i < count; i++)
        {
            //if (i == 0)
            //{
            //    var list = players.Where(p => p.Name.Contains("Evangelista")
            //                               || p.Name.Contains("Yago")).ToList();

            //    players.RemoveAll(p => list.Contains(p));

            //    foreach (var item in list)
            //    {
            //        AddPlayerAndUpdateScore(dic, 0, item);

            //    }
            //}

            //var key = func(dic, value, isWeak);    // this.MethodToGetTeamKey(dic);
            var key =  this.MethodToGetTeamKey(dic);

            var player = this.MethodToGetPlayer(players);

            players.Remove(player);

            if (player != null)
                AddPlayerAndUpdateScore(dic, key, player);

            if (((i+1) % 3) == 0)
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


    private void DecideMethod2(bool isWeak)
    {

        if (isWeak)
        {
            this.MethodToGetPlayer = (List<Player> players) => players.OrderBy(c => c.Score()).FirstOrDefault();
        }
        else
        {
            this.MethodToGetPlayer = (List<Player> players) => players.OrderByDescending(c => c.Score()).FirstOrDefault();
        }
    }

    private int func(Dictionary<int, Team> dic, decimal value, bool isWeak)
    {
        var a1 = dic.Select(a => new { a.Key, score = (value - a.Value.Players.Sum(v => v.Score())), count = a.Value.Players.Count() });

        if (isWeak)
        {
            var a2 = a1.OrderBy(a => a.count).ThenBy(a => a.score).FirstOrDefault().Key;
            return a2;
        }
        else
        {
            var a2 = a1.OrderBy(a => a.count).ThenByDescending(a => a.score).FirstOrDefault().Key;
            return a2;

        }

    }


    private void DecideMethod(bool isWeak)
    {
        if (isWeak)
        {
            this.MethodToGetPlayer = (List<Player> players) => players.OrderBy(c => c.Score()).FirstOrDefault();
            this.MethodToGetTeamKey = (Dictionary<int, Team> dic) => dic.OrderBy(d => d.Value.Players.Count).ThenByDescending(d => d.Value.Players.Sum(v => v.Score())).FirstOrDefault().Key;
        }
        else
        {
            this.MethodToGetPlayer = (List<Player> players) => players.OrderByDescending(c => c.Score()).FirstOrDefault();
            this.MethodToGetTeamKey = (Dictionary<int, Team> dic) => dic.OrderBy(d => d.Value.Players.Count).ThenBy(d => d.Value.Players.Sum(v => v.Score())).FirstOrDefault().Key;
        }
    }
}
