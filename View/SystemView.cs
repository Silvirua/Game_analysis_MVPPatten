using Game_analysis_MVPPattern.Presenter;

namespace Game_analysis_MVPPattern.View{
  public class SystemView{
    public event Action<int> OnInputReceived;

    private int playerFreegems = 0;
    private int playerPaygems = 0;
    private isRunning = false;
    
    public void StartGameLoop(){
      isRunning = true;
      DrawStatus();

      while(isRunning){
        Console.Write(" 동작을 선택하세요 (1. 1회 뽑기, 2: 10회 뽑기, 3: 종료) : ");

        string input = Console.ReadLine();

        if(int.TryParse(input, out int actionCode) && actionCode >= 1 && actionCode <= 3){
          OnInputReceived?.Invoke(actionCode);
          if(isRunning){
            DrwaStatus();
          }
        }else{
          Console.WriteLine("잘못된 입력입니다.");
        }
      }
    }

    public void ShowGameExit(){
      DrawStatus();
      Console.WriteLine( isGameExit ? "\n>>> Game Exit! <<<" : "\n\n");
    }
    
    private void DrawStatus(){
      Console.WriteLine("  ============================================  ");
      Console.WriteLine($"[무료 재화 : {}, 유료 재화 : {}]");
      Console.WriteLine("  ============================================  ");
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
