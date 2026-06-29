using UnityEngine;

[CreateAssetMenu(menuName = "Leaderboard/Icon Providers/Tier Icon Provider")]
public class TierIconProvider : LeaderboardIconProviderBase, ITierIconProvider
{
    public Sprite GetTierIcon(int tier)
    {
        return GetIcon(tier);
    }
}
