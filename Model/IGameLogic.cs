namespace Game_analysis_MVPPattern.Model{
    internal interface IGameLogic{
        int Fgem{get;}
        int Pgem{get;}

        // 플레이어 무료 재화 변경 이벤트
        public event Action<int> OnPlayerFgemChanged;
        // 플레이어 유료 재화 변경 이벤트
        public event Action<int> OnPlayerPgemChanged;

        // 게임 종료 상태 발생 이벤트
        public event Action<bool> OnGameExit;

        // 플레이어 봅기 결과 메세지 발생 이벤트
        public event Action<string> OnDrawResultLog;

        // 뽑기 프로세스
        public void DrawProcess(ActionType playerAction);
}
