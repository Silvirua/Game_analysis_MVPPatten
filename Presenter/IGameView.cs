namespace MVPPattern.Presenter
{
    internal interface IGameView
    {
        event Action<int> OnInputReceived;

        void UpdatePlayerFGems(int fgems);

        void UpdatePlayerPGems(int pgems);

        void PrintLog(string message);

        void ShowGameExit();

        void StartGameLoop();
    }
}
