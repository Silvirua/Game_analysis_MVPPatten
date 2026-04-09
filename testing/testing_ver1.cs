using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVPPattern.Presenter;

namespace MVPPattern.View
{
    internal class SystemView
    {
        public event Action<int> OnInputReceived;

        // 유료 재화, 무료 재화, 게임 진행 여부
        private int playerFreegems = 0;
        private int playerPaygems = 0;
        private bool isRunning = false;


        // 게임 루프
        public void StartGameLoop()
        {
            isRunning = true;
            DrawStatus();

            // 루프 부분
            while (isRunning)
            {
                Console.Write(" 동작을 선택하세요 (1: 1회 뽑기, 2: 10회 뽑기, 3: 종료) : ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int actionCode) && actionCode >= 1 && actionCode <= 3)
                {
                    OnInputReceived?.Invoke(actionCode);
                    if (isRunning)
                    {
                        DrawStatus();
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        // 게임 오버 여부가 없으므로, 게임 종료를 따로 지정
        public void ShowGameExit(bool isGameExit)
        {
            DrawStatus();
            Console.WriteLine(isGameExit ? "\n>>> Game Exit! <<<" : "\n\n");

        }

        // 플레이어가 가지고 있는 재화의 갯수 출력 부분
        private void DrawStatus() {
            Console.WriteLine("  =============================================  ");
            Console.WriteLine($"[무료 재화 : {playerFreegems}, 유료 재화 : {playerPaygems}]");
            Console.WriteLine("  =============================================  ");
        }
    }
}
