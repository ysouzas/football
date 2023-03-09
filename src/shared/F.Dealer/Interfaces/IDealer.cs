﻿using F.Models;

namespace F.Dealer.Interfaces;

public interface IDealer
{
    public Dictionary<int, Team> SortTeams(List<Player> players, int numberOfTeams);

    public Dictionary<int, Team> SortTeamsRandom(List<Player> players, int numberOfTeams);
}
