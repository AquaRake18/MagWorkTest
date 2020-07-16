using UnityEngine;

public class LinkerLine
    : MonoBehaviour
    , IPooledObject {
    // IPooledObject
    public void OnSpawn() {
        transform.rotation = Quaternion.identity;
    }

    public void CenterPosition(Vector3 pos1, Vector3 pos2) {
        transform.position = (pos1 + pos2) / 2;
        float angle = Mathf.Atan2(pos2.y-pos1.y, pos2.x-pos1.x) * 180 / Mathf.PI;
        transform.Rotate(new Vector3(0f, 0f, angle));
    }
}
