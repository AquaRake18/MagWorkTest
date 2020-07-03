using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class LevelCollection {
    public LevelData[] _StoredLevels;

    public int Count {
        get {
            if (_StoredLevels == null) {
                return 0;
            } else {
                return _StoredLevels.Length;
            }
        }
    }

    public LevelCollection(LevelData[] levelData) {
        if (!ValidateLevels(levelData)) {
            Debug.LogError("Failed to load leveldata due mismatch in levelID's.");
            levelData = null;
        }
        _StoredLevels = levelData;
    }

    private bool ValidateLevels(LevelData[] levelData) {
        if (levelData == null) {
            return true;
        }
        for (int levelIndex = 0; levelIndex < levelData.Length; ++levelIndex) {
            if (levelData[levelIndex] == null) {
                return false;
            } else if (levelData[levelIndex]._LevelID != (levelIndex + 1)) {
                return false;
            }
        }
        return true;
    }

    public bool AddLevel(LevelData newData) {
        if (newData != null) {
            if (_StoredLevels == null) {
                // this is first entry
                _StoredLevels = new LevelData[] { newData };
            } else if (newData._LevelID <= _StoredLevels.Length) {
                // level already exists
                return ReplaceLevel(newData);
            } else {
                // new level to be added last
                List<LevelData> list = _StoredLevels.OfType<LevelData>().ToList();
                list.Add(newData);
                if (ValidateLevels(list.ToArray())) {
                    _StoredLevels = list.ToArray();
                    return true;
                } else {
                    Debug.LogWarning("Tried to add level with already existing levelID: " + newData._LevelID);
                }
            }
        }
        return false;
    }

    public bool DeleteLevel(int levelID) {
        if (_StoredLevels != null
            && levelID <= _StoredLevels.Length) {
            bool found = false;
            List<LevelData> list = new List<LevelData>();
            for (int levelIndex = 0; levelIndex < _StoredLevels.Length; ++levelIndex) {
                if (_StoredLevels[levelIndex]._LevelID == levelID) {
                    found = true;
                } else if (found) {
                    --_StoredLevels[levelIndex]._LevelID;
                    list.Add(_StoredLevels[levelIndex]);
                } else {
                    list.Add(_StoredLevels[levelIndex]);
                }
            }
            _StoredLevels = list.ToArray();
            return true;
        }
        return false;
    }

    private bool ReplaceLevel(LevelData levelData) {
        if (levelData != null
            && _StoredLevels != null
            && levelData._LevelID <= _StoredLevels.Length) {
            _StoredLevels[levelData._LevelID - 1] = levelData;
            return true;
        }
        return false;
    }

    public LevelData GetLevel(int levelID) {
        if (_StoredLevels != null
            && levelID <= _StoredLevels.Length) {
            return _StoredLevels[levelID - 1];
        }
        return null;
    }

    public Dictionary<int, LevelData> GetLevelList() {
        Dictionary<int, LevelData> levelCollection = new Dictionary<int, LevelData>();
        if (_StoredLevels != null) {
            for (int levelIndex = 0; levelIndex < _StoredLevels.Length; ++levelIndex) {
                levelCollection[levelIndex + 1] = _StoredLevels[levelIndex];
            }
        }
        return levelCollection;
    }
}
