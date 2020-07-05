public class LevelProgress {
    private int _MovesLeft;
    public int MovesLeft {
        get { return _MovesLeft; }
        set { _MovesLeft = value; }
    }

    private int _CurrentScore;
    public int CurrentScore {
        get { return _CurrentScore; }
        set { _CurrentScore = value; }
    }

    public LevelProgress(int movesLeft) {
        _MovesLeft = movesLeft;
        _CurrentScore = 0;
    }
}
