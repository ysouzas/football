using F.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F.Dealer.Extensions;

public static class TeamsExtensions
{
    public static void AddPlayerAndUpdateScore(this Team team, Player player)
    {
        team.Players.Add(player);
        team.Score += player.MomentScore();
    }
}
