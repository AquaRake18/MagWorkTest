using UnityEngine;

public class Settings : MonoBehaviour {
    [Range(3,9)]
    public int _BoardWidth = 6;
    [Range(3,9)]
    public int _BoardHeight = 9;
    [Range(2,5)]
    public int _Colors = 5;
    public int _ClearScore = 1000;
    public int _Moves = 24;
}
