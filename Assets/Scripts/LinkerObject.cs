using UnityEngine;

public class LinkerObject : MonoBehaviour {
	public enum ELinkerState : int {
		Idle = 0,
		Focused,
		Linked,
		Destroy,
		Falling
	}

	private Animation _Animaton;

	private ELinkerState _LinkerState;
	private LinkerLogic _LinkerLogic = null;
	public SGridCoords _GridCoords;
	private float _FallSpeed;
	private float _FallStartTime;
	private float _FallJourney;
	private Vector3 _FallFromPosition;
	private Vector3 _FallDestination;

	public void Initialize(LinkerLogic linkerLogic) {
		_LinkerLogic = linkerLogic;
	}

	void Awake() {
		_Animaton = gameObject.GetComponent<Animation>();
		_Animaton["LinkerIdle"].speed = 0.15f;
		SetState(ELinkerState.Idle);
	}

    void OnMouseDown() {
		if (_LinkerLogic.AddLinker(this)) {
			SetState(ELinkerState.Focused);
		}
    }

    void OnMouseUp() {
		_LinkerLogic.ConfirmLink();
    }

	void OnMouseEnter() {
		if (_LinkerLogic.HasActiveLink()
			&& _LinkerLogic.AddLinker(this)) {
			SetState(ELinkerState.Focused);
		}
	}

	void Update() {
		if (_LinkerState == ELinkerState.Destroy) {
			Destroy(gameObject);
		} else if (_LinkerState == ELinkerState.Falling) {
			float distCovered = (Time.time - _FallStartTime) * _FallSpeed;
			float fractionOfJourney = distCovered / _FallJourney;
			transform.position = Vector3.Lerp(_FallFromPosition, _FallDestination, fractionOfJourney);
			if (fractionOfJourney >= 1f) {
				transform.position = _FallDestination;
				SetState(ELinkerState.Idle);
			}
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
		SetState(ELinkerState.Falling);
	}

	public void SetFocused() {
		SetState(ELinkerState.Focused);
	}

	public void SetLinked() {
		SetState(ELinkerState.Linked);
	}

	public void CancelLink() {
		SetState(ELinkerState.Idle);
	}

	public void ConfirmLink() {
		SetState(ELinkerState.Destroy);
	}

	private void SetState(ELinkerState state) {
		_LinkerState = state;
		switch (state) {
			case ELinkerState.Focused:
			_Animaton.Play("LinkerFocused");
			break;
			case ELinkerState.Linked:
			_Animaton.Play("LinkerLinked");
			break;
			default:
			_Animaton.Play("LinkerIdle");
			break;
		}
	}
}
