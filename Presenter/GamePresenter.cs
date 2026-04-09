using Game_analysis_MVPPattern.Model;

namespace Game_analysis_MVPPattern.Presenter{
  public class GamePresenter{
    private IGameView _view;
    private IGameLogic _model;

    public GamePresenter(IGameView view, IGameLogic model){
      _view = view;
      _model = model;
      Init();
    }

    private void Init(){
      _view.OnInputReceived += HandlePlayerInput;

      _model.OnDrawResult += log => _view.PrintLog(log);
    }

    private void HandlePlayerInput(int inputCode){
      _model.Process

}
