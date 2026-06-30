public interface ICurrentPlayerProfileProvider
{
    public CurrentPlayerProfilePayload GetCurrentPlayerProfile();

    public string GetCurrentPlayerId();

    public void SetCurrentPlayerProfile(CurrentPlayerProfilePayload currentPlayerProfile);
}
