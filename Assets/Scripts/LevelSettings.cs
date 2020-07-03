using UnityEngine;

public class LevelSettings : MonoBehaviour {
    public readonly int _MaxWidth = 9;
    public readonly int _MaxHeight = 9;
    [Range(3, 9)]
    public int _BoardWidth = 6;
    [Range(3, 9)]
    public int _BoardHeight = 9;
    [Range(2,5)]
    public int _LinkerColors = 5;
    public int _TargetScore = 1000;
    public int _Moves = 24;

    private LevelCollection _LevelData;

    void Awake() {
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
