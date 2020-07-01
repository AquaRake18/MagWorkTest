using UnityEngine;

public class LinkerObject : MonoBehaviour {
	private Vector3 _BeginTouchPosition;
    private Vector3 _EndTouchPosition;
    private float _Angle;
    private EDirection _Direction;
	private bool _ActiveLink = false;
	public bool ActiveLink {
		get { return _ActiveLink; }
	}

    void OnMouseDown() {
        _BeginTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		ActivateLink();
    }

	void OnMouseEnter() {
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
		_ActiveLink = true;
	}

	public void CancelLink() {
		_ActiveLink = false;
	}

	public void ConfirmLink() {
		_ActiveLink = false;
	}
}
