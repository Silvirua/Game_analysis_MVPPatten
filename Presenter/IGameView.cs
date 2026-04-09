namespace Game_analysis_MVPPattern.Presenter{
  public interface IGameView{
    event Action<int> OnInputReceived;

    // Player가 가지고 있는 재화 갱신
    void UpdatePlayerGems(int gem); 

    // Log 표시
    void PrintLog(string message);

    // 게임 프로세스
    void StartGameLoop();
  }
}
