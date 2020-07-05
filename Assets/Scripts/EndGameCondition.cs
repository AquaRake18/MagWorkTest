public class EndGameCondition {
    public enum EGameResult {
        Running,
        Failure,
        FailureShuffle,
        Success
    }

    private EGameMode _GameMode;
    private LevelSettings _LevelSettings;
    private LevelProgress _LevelProgress;
    private bool _ShuffleFailure;

    public EndGameCondition(
        EGameMode gameMode,
        LevelSettings settings,
        LevelProgress progress) {
        _GameMode = gameMode;
        _LevelSettings = settings;
        _LevelProgress = progress;
        _ShuffleFailure = false;
    }

    public void SetShuffleFailure() {
        _ShuffleFailure = true;
    }

    public EGameResult GetGameResult() {
        if (_ShuffleFailure) {
            return EGameResult.FailureShuffle;
        }
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
