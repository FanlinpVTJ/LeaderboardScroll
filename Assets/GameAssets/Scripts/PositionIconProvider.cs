using UnityEngine;

[CreateAssetMenu(menuName = "Leaderboard/Icon Providers/Position Icon Provider")]
public class PositionIconProvider : LeaderboardIconProviderBase, IPositionIconProvider
{
    public Sprite GetPositionIcon(int position)
    {
        return GetIcon(position);
    }
}
