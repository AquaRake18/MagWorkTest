using UnityEngine;

public class LinkerTouchArea : MonoBehaviour {
    private Vector3 _FirstTouchPosition;
    private Vector3 _FinalTouchPosition;
    public float _Angle;
    public EDirection _Direction;

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
        _Direction = Direction.AngleToDirection(_Angle);
    }
}
