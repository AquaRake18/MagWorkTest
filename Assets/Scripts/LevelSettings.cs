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
    public int _ClearScore = 1000;
    public int _Moves = 24;
}
