using UnityEngine;
using UnityEditor;

public class LevelEditorWindow : EditorWindow {
    private int _LevelID;
    private int _Width;
    private int _Height;
    private int _LinkerColors;
    private int _TargetScore;
    private int _Moves;

    private readonly int _MinWidth = 3;
    private readonly int _MinHeight = 3;
    private readonly int _MaxWidth = 9;
    private readonly int _MaxHeight = 9;
    private readonly int _MinLinkerColors = 2;
    private readonly int _MaxLinkerColors = 5;

    private bool _EditingLevel;
    private int _LevelSliderValue;
    private readonly int _LevelSliderMin = 1;
    private int _LevelSliderMax;

    private LevelCollection _LevelCollection = null;

    [MenuItem("Window/Linker Game/Level Editor")]
    public static void ShowWindow() {
        GetWindow<LevelEditorWindow>("Level Editor");
    }

    void OnGUI() {
        if (_LevelCollection == null) {
            ReloadLevelCollection();
        }
        if (GUILayout.Button("Reload", GUILayout.Width(64f))) {
            ReloadLevelCollection();
        }

        GUILayout.Space(14f);

        if (GUILayout.Button("Create New", GUILayout.Width(82f))) {
            CreateNew();
        }

        EditorGUI.BeginDisabledGroup(_LevelSliderMax <= 1);
        {
            if (GUILayout.Button("Load", GUILayout.Width(82f))) {
                LoadLevel();
            }
            _LevelSliderValue = EditorGUILayout.IntSlider(
                _LevelSliderValue,
                _LevelSliderMin,
                _LevelSliderMax,
                GUILayout.Width(260)
            );
        }
        EditorGUI.EndDisabledGroup();

        GUILayout.Space(14f);

        EditorGUI.BeginDisabledGroup(!_EditingLevel);
        {
            GUILayout.Label("Level: " + _LevelID, EditorStyles.boldLabel);
            _Width = EditorGUILayout.IntSlider(
                "Board width",
                _Width,
                _MinWidth,
                _MaxWidth,
                GUILayout.Width(260)
            );
            _Height = EditorGUILayout.IntSlider(
                "Board Height",
                _Height,
                _MinHeight,
                _MaxHeight,
                GUILayout.Width(260)
            );
            _LinkerColors = EditorGUILayout.IntSlider(
                "Linker Colors",
                _LinkerColors,
                _MinLinkerColors,
                _MaxLinkerColors,
                GUILayout.Width(260)
            );
            _TargetScore = EditorGUILayout.IntField("Target score:", _TargetScore);
            _Moves = EditorGUILayout.IntField("Moves:", _Moves);
            if (GUILayout.Button("Save", GUILayout.Width(64f))) {
                SaveLevel();
            }
        }
        EditorGUI.EndDisabledGroup();
    }

    private void ReloadLevelCollection() {
        _EditingLevel = false;
        _LevelCollection = SaveSystem.LoadLevels();
        Debug.Log("Reload");
        if (_LevelCollection == null) {
            _LevelCollection = new LevelCollection(null);
            if (_LevelCollection._StoredLevels != null) {
                _LevelSliderMax = _LevelCollection._StoredLevels.Length;
            } else {
                _LevelSliderMax = 1;
            }
            Debug.Log("Loaded levels = " + _LevelSliderMax);
        } else {
            Debug.Log("LevelCollection is NULL");
        }
    }

    private void CreateNew() {
        _EditingLevel = true;
        _LevelID = GetNextLevelID();
        _Width = 3;
        _Height = 3;
        _LinkerColors = 5;
        _TargetScore = 10000;
        _Moves = 10;
    }

    private void LoadLevel() {
        _EditingLevel = true;
        _LevelID = GetNextLevelID();
        _Width = 3;
        _Height = 3;
        _LinkerColors = 5;
        _TargetScore = 10000;
        _Moves = 10;
    }

    private void SaveLevel() {
        LevelData editData = new LevelData(
            _LevelID,
            _Width,
            _Height,
            _LinkerColors,
            _TargetScore,
            _Moves
        );
        if (_LevelCollection.AddLevel(editData)) {
            SaveSystem.SaveLevels(_LevelCollection);
            ReloadLevelCollection();
        }
    }

    private int GetNextLevelID() {
        if (_LevelCollection._StoredLevels != null) {
            return _LevelCollection._StoredLevels.Length + 1;
        }
        return 1;
    }
}
