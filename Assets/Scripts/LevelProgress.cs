public static class LevelProgress {
    public static int _MovesLeft;
    public static int _CurrentScore;

    public static void Reset() {
        _MovesLeft = LevelSettings.Instance.Moves;
        _CurrentScore = 0;
    }
}
