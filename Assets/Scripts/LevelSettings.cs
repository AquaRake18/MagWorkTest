public class LevelSettings : Singleton<LevelSettings> {
    // Singleton
    protected LevelSettings() {}

    public readonly int _MaxWidth = 9;
    public readonly int _MaxHeight = 9;

    private int _BoardWidth = 6;
    private int _BoardHeight = 9;
    private int _LinkerColors = 5;
    private int _TargetScore = 1000;
    private int _Moves = 24;
    private EGameMode _GameMode = EGameMode.Score;

    public int BoardWidth { get { return _BoardWidth; } }
    public int BoardHeight { get { return _BoardHeight; } }
    public int LinkerColors { get { return _LinkerColors; } }
    public int TargetScore { get { return _TargetScore; } }
    public int Moves { get { return _Moves; } }
    public EGameMode GameMode { get { return _GameMode; } }

    private LevelCollection _LevelData;

    public void Initialize() {
        UserData userData = SaveSystem.LoadUserData();
        if (userData != null) {
            _LevelData = SaveSystem.LoadLevels();
            Reset(_LevelData.GetLevel(userData._CurrentLevel));
        }
    }

    public void Reset(LevelData levelData) {
        if (levelData != null) {
            _BoardWidth = levelData._Width;
            _BoardHeight = levelData._Height;
            _LinkerColors = levelData._LinkerColors;
            _TargetScore = levelData._TargetScore;
            _Moves = levelData._Moves;
        }
    }
}
