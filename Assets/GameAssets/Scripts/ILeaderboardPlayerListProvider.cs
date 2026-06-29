using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface ILeaderboardPlayerListProvider
{
    public List<PlayerInfoPayload> GetPlayerInfoList();

    public PlayerInfoPayload GetPlayerInfoPayload(int index);

    public UniTask<List<PlayerInfoPayload>> LoadPlayerInfoListAsync();

    public void SetPlayerInfoList(List<PlayerInfoPayload> playerInfoList);
}
