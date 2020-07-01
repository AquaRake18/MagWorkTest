using UnityEngine;

public class LinkerObject : MonoBehaviour {
	public enum ELinkerTypeID {
		Red,
		Blue,
		Green,
		Yellow,
		Purple
	}

	public enum ELinkerState {
		Inactive,
		Focused,
		Linked,
		Destroy
	}

	private SpriteRenderer _Sprite;
	private Color _DefaultColor;

	private Vector3 _BeginTouchPosition;
    private Vector3 _EndTouchPosition;
    private float _Angle;
    private EDirection _Direction;
	private ELinkerState _LinkerState = ELinkerState.Inactive;
	private LinkerLogic _LinkerLogic = null;
	public ELinkerTypeID _LinkerTypeID;
	public int _ArrayID;

	public void Reset(LinkerLogic linkerLogic, int arrayID) {
		_LinkerLogic = linkerLogic;
		_ArrayID = arrayID;
	}

	void Awake() {
		_Sprite = gameObject.GetComponent<SpriteRenderer>();
		_DefaultColor = _Sprite.color;
	}

    void OnMouseDown() {
        _BeginTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (_LinkerLogic.AddLinker(this)) {
			_LinkerState = ELinkerState.Focused;
		}
    }

    void OnMouseUp() {
        _EndTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
		_LinkerLogic.ConfirmLink();
    }

	void OnMouseEnter() {
		if (_LinkerLogic.HasActiveLink()
			&& _LinkerLogic.AddLinker(this)) {
			_LinkerState = ELinkerState.Focused;
		}
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
		switch (_LinkerState) {
			case ELinkerState.Inactive:
				_Sprite.color = _DefaultColor;
			break;
			case ELinkerState.Focused:
				_Sprite.color = new Color(0f, 0f, 0f, 255f);
			break;
			case ELinkerState.Linked:
				_Sprite.color = new Color(255f, 255f, 255f, 255f);
			break;
		}
	}

	public void SetFocused() {
		_LinkerState = ELinkerState.Focused;
	}

	public void SetLinked() {
		_LinkerState = ELinkerState.Linked;
	}

	public void CancelLink() {
		_LinkerState = ELinkerState.Inactive;
	}

	public void ConfirmLink() {
		_LinkerState = ELinkerState.Destroy;
	}
}
