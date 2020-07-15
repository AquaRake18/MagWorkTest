using UnityEngine;

public class LinkerObject
	: MonoBehaviour
	, IPooledObject {
	public Sprite[] _ColorSprites;
	public SGridCoords _GridCoords;

	private Animation _Animaton;
	private SpriteRenderer _SpriteRenderer;

	private ELinkerState _LinkerState;
	private LinkerLogic _LinkerLogic;
	private float _TimeToDie = .35f;
	private float _DestroyTime;
	private float _FallSpeed;
	private float _FallStartTime;
	private float _FallJourney;
	private Vector3 _FallFromPosition;
	private Vector3 _FallDestination;

	private GameObject _Spinner = null;

	public void Initialize(LinkerLogic linkerLogic) {
		_LinkerLogic = linkerLogic;
	}

	// IPooledObject
	public void OnSpawn() {
		SetState(ELinkerState.Idle);
		_SpriteRenderer.sprite = _ColorSprites[Random.Range(0, Mathf.Clamp(LevelSettings.Instance.LinkerColors, 0, _ColorSprites.Length))];
		gameObject.tag = _SpriteRenderer.sprite.name;
	}

	void Awake() {
		_Animaton = gameObject.GetComponent<Animation>();
		_Animaton["LinkerIdle"].speed = 0.15f;
		_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
		if (_LinkerState == ELinkerState.Destroy
			&& Time.time >= _DestroyTime) {
			gameObject.SetActive(false);
			gameObject.transform.SetParent(ObjectPooler.Instance.gameObject.transform);
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
		switch (_LinkerState) {
			case ELinkerState.Focused:
				_Animaton.Play("LinkerFocused");
				_Spinner = ObjectPooler.Instance.SpawnFromPool(ObjectPoolTypes.FocusedSpinner);
				_Spinner.transform.position = transform.position;
				break;
			case ELinkerState.Linked:
				_Animaton.Play("LinkerLinked");
				break;
			case ELinkerState.Destroy:
				_Animaton.Play("LinkerDestroyed");
				_DestroyTime = Time.time + _TimeToDie;
				break;
			default:
				_Animaton.Play("LinkerIdle");
				gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "LinkerDestroyed";
				break;
		}
		if (_LinkerState != ELinkerState.Focused
			&& _Spinner != null) {
			_Spinner.SetActive(false);
			_Spinner = null;
		}
	}
}
