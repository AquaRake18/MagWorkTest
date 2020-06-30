using UnityEngine;

public class LinkerObject : MonoBehaviour {
    private ELinkerType _LinkerType = ELinkerType.Blue;
    private SpriteRenderer _Sprite;

    void Awake() {
        _Sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Initialize(LevelSettings settings) {
        _LinkerType = (ELinkerType)Random.Range(0, settings._LinkerColors);
        _Sprite.color = LinkerTypeUtil.GetColor(_LinkerType);
    }
}
