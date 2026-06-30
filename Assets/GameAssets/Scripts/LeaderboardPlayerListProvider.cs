using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class LeaderboardPlayerListProvider : ILeaderboardPlayerListProvider
{
    private readonly TextAsset _playerInfoJsonFile;
    private readonly int _loadingDelayMilliseconds;
    private List<PlayerInfoPayload> _playerInfoList;

    public LeaderboardPlayerListProvider(TextAsset playerInfoJsonFile, int loadingDelayMilliseconds)
    {
        _playerInfoJsonFile = playerInfoJsonFile;
        _loadingDelayMilliseconds = loadingDelayMilliseconds;
        _playerInfoList = new List<PlayerInfoPayload>();
    }

    public List<PlayerInfoPayload> GetPlayerInfoList()
    {
        return _playerInfoList;
    }

    public PlayerInfoPayload GetPlayerInfoPayload(int index)
    {
        return _playerInfoList[index];
    }

    public int GetPlayerIndex(string playerId)
    {
        for (int i = 0; i < _playerInfoList.Count; i++)
        {
            if (_playerInfoList[i].Id == playerId)
            {
                return i;
            }
        }

        throw new KeyNotFoundException($"Player with id {playerId} was not found in leaderboard.");
    }

    public async UniTask<List<PlayerInfoPayload>> LoadPlayerInfoListAsync()
    {
        await UniTask.Delay(_loadingDelayMilliseconds);

        if (_playerInfoList.Count == 0)
        {
            _playerInfoList = JsonConvert.DeserializeObject<List<PlayerInfoPayload>>(_playerInfoJsonFile.text);
        }

        return _playerInfoList;
    }

    public void SetPlayerInfoList(List<PlayerInfoPayload> playerInfoList)
    {
        _playerInfoList = playerInfoList;
    }
}
