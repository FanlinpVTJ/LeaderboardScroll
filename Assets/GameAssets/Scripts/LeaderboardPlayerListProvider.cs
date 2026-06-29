using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class LeaderboardPlayerListProvider : ILeaderboardPlayerListProvider
{
    private TextAsset _playerInfoJsonFile;
    private List<PlayerInfoPayload> _playerInfoList;

    public LeaderboardPlayerListProvider(TextAsset playerInfoJsonFile)
    {
        _playerInfoJsonFile = playerInfoJsonFile;
        _playerInfoList = JsonConvert.DeserializeObject<List<PlayerInfoPayload>>(_playerInfoJsonFile.text);
    }

    public List<PlayerInfoPayload> GetPlayerInfoList()
    {
        return _playerInfoList;
    }

    public void SetPlayerInfoList(List<PlayerInfoPayload> playerInfoList)
    {
        _playerInfoList = playerInfoList;
    }
}
