using System.Collections.Generic;

public class LinkerLogic {
    private readonly int _MinimumLinks = 3;
    private LinkerObject[,] _BoardObjectsRef;
    private List<LinkerObject> _LinkedObjects = new List<LinkerObject>();

    public void Initialize(ref LinkerObject[,] boardObjects) {
        _BoardObjectsRef = boardObjects;
    }

    public bool HasActiveLink() {
        return _LinkedObjects.Count > 0;
    }

    public bool AddLinker(LinkerObject linkerObject) {
        if (!HasActiveLink()
            || (!_LinkedObjects.Contains(linkerObject)
            && linkerObject._LinkerTypeID == _LinkedObjects[0]._LinkerTypeID
            && IsAdjacent(linkerObject))) {
            _LinkedObjects.Add(linkerObject);
            return true;
        }
        return false;
    }

    public bool IsAdjacent(LinkerObject other) {
        LinkerObject lastElement = _LinkedObjects[_LinkedObjects.Count - 1];
        return true;
    }

    public void ConfirmLink() {
        if (_LinkedObjects.Count < _MinimumLinks) {
            foreach (LinkerObject obj in _LinkedObjects) {
                obj.CancelLink();
            }
        } else {
            foreach (LinkerObject obj in _LinkedObjects) {
                obj.ConfirmLink();
            }
        }
        _LinkedObjects.Clear();
    }
}
