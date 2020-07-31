using UnityEngine;

public class TimeToDie
    : MonoBehaviour
    , IPooledObject {
    public float _Duration = .6f;
    private float _TimeToDie;

    public void OnSpawn() {
        _TimeToDie = Time.time + _Duration;
    }

    void Update() {
        if (Time.time >= _TimeToDie) {
            gameObject.SetActive(false);
        }
    }
}
