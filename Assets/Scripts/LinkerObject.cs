using UnityEngine;

public class LinkerObject : MonoBehaviour {
	public enum ELinkerState {
		Inactive,
		Focused,
		Linked,
		Destroy,
		Falling
	}

	private SpriteRenderer _Sprite;
	private Color _DefaultColor;

	private ELinkerState _LinkerState = ELinkerState.Inactive;
	private LinkerLogic _LinkerLogic = null;
	public SGridCoords _GridCoords;
	private float _FallSpeed;
	private float _FallStartTime;
	private float _FallJourney;
	private Vector3 _FallFromPosition;
	private Vector3 _FallDestination;

	public void Reset(LinkerLogic linkerLogic, SGridCoords gridCoords) {
		_LinkerLogic = linkerLogic;
		_GridCoords = gridCoords;
	}

	void Awake() {
		_Sprite = gameObject.GetComponent<SpriteRenderer>();
		_DefaultColor = _Sprite.color;
	}

    void OnMouseDown() {
		if (_LinkerLogic.AddLinker(this)) {
			_LinkerState = ELinkerState.Focused;
		}
    }

    void OnMouseUp() {
		_LinkerLogic.ConfirmLink();
    }

	void OnMouseEnter() {
		if (_LinkerLogic.HasActiveLink()
			&& _LinkerLogic.AddLinker(this)) {
			_LinkerState = ELinkerState.Focused;
		}
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
			case ELinkerState.Falling:
				float distCovered = (Time.time - _FallStartTime) * _FallSpeed;
				float fractionOfJourney = distCovered / _FallJourney;
				transform.position = Vector3.Lerp(_FallFromPosition, _FallDestination, fractionOfJourney);
				if (fractionOfJourney >= 1f) {
					transform.position = _FallDestination;
					_LinkerState = ELinkerState.Inactive;
				}
			break;
		}
	}

	public bool IsDestroyed() {
		return _LinkerState == ELinkerState.Destroy;
	}

	public bool IsFalling() {
		return _LinkerState == ELinkerState.Falling;
	}

	public void SetFalling(float fallSpeed, Vector3 fallDestination, SGridCoords destGridCoords) {
		_FallSpeed = fallSpeed;
		_FallStartTime = Time.time;
		_FallFromPosition = gameObject.transform.position;
		_FallDestination = fallDestination;
		_FallJourney = Vector3.Distance(_FallFromPosition, _FallDestination);
		_GridCoords = destGridCoords;
		_LinkerState = ELinkerState.Falling;
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
