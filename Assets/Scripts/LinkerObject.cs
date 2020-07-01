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

	private ELinkerState _LinkerState = ELinkerState.Inactive;
	private LinkerLogic _LinkerLogic = null;
	public ELinkerTypeID _LinkerTypeID;
	public SGridCoords _GridCoords;

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
