using UnityEngine;

[CreateAssetMenu(menuName = "Leaderboard/Icon Providers/Guild Icon Provider")]
public class GuildIconProvider : LeaderboardIconProviderBase, IGuildIconProvider
{
    public Sprite GetGuildTierIcon(int guildTier)
    {
        return GetIcon(guildTier);
    }
}
