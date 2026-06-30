public class CurrentPlayerProfileProvider : ICurrentPlayerProfileProvider
{
    private CurrentPlayerProfilePayload _currentPlayerProfile;

    public CurrentPlayerProfileProvider(string currentPlayerId)
    {
        CurrentPlayerProfilePayload currentPlayerProfile = new CurrentPlayerProfilePayload();
        currentPlayerProfile.Id = currentPlayerId;
        _currentPlayerProfile = currentPlayerProfile;
    }

    public CurrentPlayerProfilePayload GetCurrentPlayerProfile()
    {
        return _currentPlayerProfile;
    }

    public string GetCurrentPlayerId()
    {
        return _currentPlayerProfile.Id;
    }

    public void SetCurrentPlayerProfile(CurrentPlayerProfilePayload currentPlayerProfile)
    {
        _currentPlayerProfile = currentPlayerProfile;
    }
}
