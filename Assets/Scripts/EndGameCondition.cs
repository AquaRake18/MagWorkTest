public class EndGameCondition {
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

    public bool ConditionsMet() {
        bool result = false;
        switch (_GameMode) {
            case EGameMode.Score:
            result = (_LevelProgress.CurrentScore >= _LevelSettings.TargetScore);
            break;
        }
        return result;
    }
}
