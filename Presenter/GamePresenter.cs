using MVPPattern.Model;

namespace Game_analysis_MVPPattern.Presenter
{
    internal class GamePresenter
    {
        private IGameView _view;
        private IGameLogic _model;

        public GamePresenter(IGameView view, IGameLogic model)
        {
            _view = view;
            _model = model;
            Init();
        }

        private void Init()
        {
            _view.UpdatePlayerFGems(_model.Fgem);
            _view.UpdatePlayerPGems(_model.Pgem);

            _view.OnInputReceived += HandlePlayerInput;

            _model.OnPlayerPgemChanged += (amount) => _view.UpdatePlayerPGems(amount);
            _model.OnPlayerFgemChanged += (amount) => _view.UpdatePlayerFGems(amount);

            _model.OnDrawResultLog += (message) => _view.PrintLog(message);
            _model.OnGameExit += (isExit) => { if (isExit) _view.ShowGameExit(); };
        }

        private void HandlePlayerInput(int inputCode)
        {
            _model.DrawProcess((ActionType)inputCode);
        }
    }
}
