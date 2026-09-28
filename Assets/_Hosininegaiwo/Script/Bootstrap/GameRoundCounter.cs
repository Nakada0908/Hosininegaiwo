public static class GameRoundCounter
{
    //ƒQ[ƒ€‚ª2‰ñ–Ú‚©‚Ç‚¤‚©‚ð”»’è‚·‚éƒtƒ‰ƒO
    public static bool isSecondRound { get; private set; }

    public static void StartFirstGame()=> isSecondRound = false;
    public static void StartSecondGame()=> isSecondRound = true;
}
