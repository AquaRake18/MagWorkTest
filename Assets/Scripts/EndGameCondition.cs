public class EndGameCondition {
    public enum EGameResult {
        Running,
        Failure,
        Success,
        EndOfContent
    }

    private EGameMode _GameMode;
    private LevelSettings _LevelSettings;
    private LevelProgress _LevelProgress;

    public EndGameCondition(
        EGameMode gameMode,
        LevelSettings settings,
        LevelProgress progress) {
        _GameMode = gameMode;
        _LevelSettings = settings;
        _LevelProgress = progress;
    }

    public EGameResult GetGameResult() {
        bool success = false;
        switch (_GameMode) {
            case EGameMode.Score:
                if (_LevelProgress.MovesLeft <= 0
                    && _LevelProgress.CurrentScore < _LevelSettings.TargetScore) {
                    return EGameResult.Failure;
                } else if (_LevelProgress.CurrentScore >= _LevelSettings.TargetScore) {
                    success = true;
                }
                break;
        }
        if (success) {
            //EndOfContent check
            return EGameResult.Success;
        }
        return EGameResult.Running;
    }
}
