using System.Collections.Generic;

public interface ILeaderboardPlayerListProvider
{
    public List<PlayerInfoPayload> GetPlayerInfoList();

    public void SetPlayerInfoList(List<PlayerInfoPayload> playerInfoList);
}
