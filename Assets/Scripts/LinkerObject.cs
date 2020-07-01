using UnityEngine;

public class LinkerObject : MonoBehaviour {
	public enum ELinkerState {
		Inactive,
		Focused,
		Linked,
		Destroy
	}

	private Vector3 _BeginTouchPosition;
    private Vector3 _EndTouchPosition;
    private float _Angle;
    private EDirection _Direction;
	private ELinkerState _LinkerState = ELinkerState.Inactive;
	public bool ActiveLink {
		get { return _LinkerState == ELinkerState.Focused || _LinkerState == ELinkerState.Linked; }
	}

    void OnMouseDown() {
        _BeginTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		ActivateLink();
    }

    void OnMouseUp() {
        _EndTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

	void OnMouseEnter() {
	}

    private void CalculateAngle() {
        _Angle = Mathf.Atan2(
            _EndTouchPosition.y - _BeginTouchPosition.y,
            _EndTouchPosition.x - _BeginTouchPosition.x
        ) * 180 / Mathf.PI;
        _Direction = Direction.AngleToDirection(_Angle);
    }

	void Update() {
		if (_LinkerState == ELinkerState.Destroy) {
			Destroy(gameObject);
		}
	}

	public void ActivateLink() {
		_LinkerState = ELinkerState.Focused;
	}

	public void CancelLink() {
		_LinkerState = ELinkerState.Inactive;
	}

	public void ConfirmLink() {
		_LinkerState = ELinkerState.Destroy;
	}
}
