public class EndGameCondition {
    public enum EGameResult {
        Running,
        Success,
        Failure
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
        switch (_GameMode) {
            case EGameMode.Score:
                if (_LevelProgress.MovesLeft <= 0
                    && _LevelProgress.CurrentScore < _LevelSettings.TargetScore) {
                    return EGameResult.Failure;
                } else if (_LevelProgress.CurrentScore >= _LevelSettings.TargetScore) {
                    return EGameResult.Success;
                }
                break;
        }
        return EGameResult.Running;
    }
}
