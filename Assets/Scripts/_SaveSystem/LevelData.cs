[System.Serializable]
public class LevelData {
    public int _LevelID;
    public int _Width;
    public int _Height;
    public int _LinkerColors;
    public int _TargetScore;
    public int _Moves;

    public LevelData(
        int levelID,
        int width,
        int height,
        int linkerColors,
        int targetScore,
        int moves) {
        _LevelID = levelID;
        _Width = width;
        _Height = height;
        _LinkerColors = linkerColors;
        _TargetScore = targetScore;
        _Moves = moves;
    }
}
