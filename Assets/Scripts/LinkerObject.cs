using UnityEngine;

public class LinkerObject : MonoBehaviour {
	private Vector3 _BeginTouchPosition;
    private Vector3 _EndTouchPosition;
    public float _Angle;
    public EDirection _Direction;

    void OnMouseDown() {
        _BeginTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp() {
        _EndTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    private void CalculateAngle() {
        _Angle = Mathf.Atan2(
            _EndTouchPosition.y - _BeginTouchPosition.y,
            _EndTouchPosition.x - _BeginTouchPosition.x
        ) * 180 / Mathf.PI;
        _Direction = Direction.AngleToDirection(_Angle);
    }

	public void ActivateLink() {
	}

	public void CancelLink() {
	}
}
