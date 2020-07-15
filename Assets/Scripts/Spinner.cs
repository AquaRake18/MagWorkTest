using UnityEngine;

public class Spinner : MonoBehaviour {
    public Vector3 _RotationSpeed = new Vector3(0f, 0f, -2f);

    void Update() {
        gameObject.transform.Rotate(_RotationSpeed);
    }
}
