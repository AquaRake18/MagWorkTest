public class LevelProgress {
    private int _CurrentScore;
    public int CurrentScore {
        get { return _CurrentScore; }
        set { _CurrentScore = value; }
    }

    public LevelProgress() {
        _CurrentScore = 0;
    }
}
