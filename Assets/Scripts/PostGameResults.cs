public static class PostGameResults {
    public enum EGameResult {
        Running,
        Failure,
        FailureShuffle,
        Success
    }

    public static bool _ShuffleFailure = false;

    public static EGameResult GetResult() {
        if (_ShuffleFailure) {
            return EGameResult.FailureShuffle;
        }
        switch (LevelSettings.Instance.GameMode) {
            case EGameMode.Score:
                if (LevelProgress._MovesLeft <= 0
                    && LevelProgress._CurrentScore < LevelSettings.Instance.TargetScore) {
                    return EGameResult.Failure;
                } else if (LevelProgress._CurrentScore >= LevelSettings.Instance.TargetScore) {
                    return EGameResult.Success;
                }
                break;
        }
        return EGameResult.Running;
    }
}
