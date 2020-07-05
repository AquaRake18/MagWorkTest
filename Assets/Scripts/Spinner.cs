using UnityEngine;

public class Spinner : MonoBehaviour {
    public Vector3 _RotationSpeed;

    void Update() {
        gameObject.transform.Rotate(_RotationSpeed);
    }
}
