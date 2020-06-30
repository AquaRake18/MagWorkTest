using UnityEngine;

public class LevelLogic : MonoBehaviour {
    public int _FPS = 60;
    private Board _Board;

    void Awake() {
        _Board = gameObject.GetComponentInChildren<Board>();
    }

    void Start() {
        Application.targetFrameRate = _FPS;
    }

    void Update() {
    }
}
