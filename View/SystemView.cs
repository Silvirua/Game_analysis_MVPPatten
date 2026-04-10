using MVPPattern.Presenter;

namespace Game_analysis_MVPPattern.View
{
    internal class SystemView : IGameView
    {
        public event Action<int> OnInputReceived;
        
        // 화면에 표시하기 위해 현재 값을 들고 있을 것
        private int PlayerFreeGems;
        private int PlayerPayGems;

        // 게임의 지속 여부
        private bool isRunning = false;

        // 무료 재화 업데이트
        public void UpdatePlayerFGems(int fgems) => PlayerFreeGems = fgems;

        // 유료 재화 업데이트
        public void UpdatePlayerPGems(int pgems) => PlayerPayGems = pgems;

        // 로그 출력
        public void PrintLog(string message)
        {
            Console.WriteLine(message);
        }

        // 게임 루프부분
        public void StartGameLoop()
        {
            isRunning = true;
            DrawStatus();

            while (isRunning)
            {
                Console.Write(" 동작을 선택하세요 (1: 1회 뽑기, 2: 10회 뽑기, 3: 종료) : ");

                string input = Console.ReadLine();

                if(int.TryParse(input, out int actionCode) && actionCode >= 1 && actionCode <= 2)
                {
                    OnInputReceived?.Invoke(actionCode);
                    if (isRunning)
                    {
                        DrawStatus();
                    }
                }else if(actionCode == 3)
                {
                    ShowGameExit();
                }
                else
                {
                    Console.WriteLine(" 잘못된 입력입니다.");
                }
            }
        }

        // 게임 오버로 게임 종료가 아닌, 게임 종료를 사용자가 직접 사용.
        public void ShowGameExit()
        {
            DrawStatus();
            Console.Write("\n\n 게임을 종료하시겠습니까? (1: 종료, 1을 제외한 수 :취소) : ");
            string exit_select = Console.ReadLine();
            if (int.TryParse(exit_select, out int exitcode) && exitcode == 1)
            {
                Console.WriteLine("\n>>> Game Exit! <<<");
                isRunning = false;
            }
            else
            {
                Console.WriteLine("\n 계속 진행합니다.");
            }
        }

        // 플레이어의 재화 갯수 여부 확인 스테이터스
        private void DrawStatus()
        {
            Console.WriteLine("  =============================================  ");
            Console.WriteLine($"        [무료 재화 : {PlayerFreeGems}, 유료 재화 : {PlayerPayGems}]");
            Console.WriteLine("  =============================================  ");
        }
    }
}




/// 해야할 것
/// view : 보여주는 형식. 그 어떤 무엇도 자발적으로 이루어질 수 없는 코드.
/// 플레이어의 재화(유료든 무료든)출력, 플레이어 선택에 따른 종료 유무

/// 기존 예제 코드 : currentPlayerHp : int = 100, currentMonsterHp : int = 100, isRunning : bool = false, DrawHpStatus() : void
/// -> playerHp 및 MonsterHp, 작동 유무, HpStatus 코드, ShowGameOver, DrawHpStatus,

/// StartGameLoop() 함수
/// private void DrawStatus() 함수 -> 재화의 갯수 유무
/// 승리 유무는 없으므로, 종료에 대한 코드가 있어야 할 것.
