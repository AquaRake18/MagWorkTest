using UnityEngine;

public class LinkerTouchArea : MonoBehaviour {
    private Vector3 _FirstTouchPosition;
    private Vector3 _FinalTouchPosition;
    public float _Angle;

    void OnMouseDown() {
        _FirstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp() {
        _FinalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    void CalculateAngle() {
        _Angle = Mathf.Atan2(
            _FinalTouchPosition.y - _FirstTouchPosition.y,
            _FinalTouchPosition.x - _FirstTouchPosition.x
        ) * 180 / Mathf.PI;
        if (_Angle > -22.5f && _Angle <= 22.5f) {
            Debug.Log("East");
        } else if (_Angle > 22.5f && _Angle <= 67.5f) {
            Debug.Log("North East");
        } else if (_Angle > 67.5f && _Angle <= 112.5f) {
            Debug.Log("North");
        } else if (_Angle > 112.5f && _Angle <= 157.5f) {
            Debug.Log("North West");
        } else if (_Angle > 157.5f || _Angle < -157.5f) {
            Debug.Log("West");
        } else if (_Angle < -112.5f && _Angle >= -157.5f) {
            Debug.Log("South West");
        } else if (_Angle < -67.5f && _Angle >= -112.5f) {
            Debug.Log("South");
        } else if (_Angle < -22.5f && _Angle >= -67.5f) {
            Debug.Log("South East");
        }
    }
}
