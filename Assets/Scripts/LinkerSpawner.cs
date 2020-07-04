using System.Collections.Generic;
using UnityEngine;

public class LinkerSpawner : MonoBehaviour {
    private LinkerLogic _LinkerLogic;
    private SGridCoords _GridCoords;
    public SGridCoords GridCoords {
        get { return _GridCoords; }
    }
    private GameObject[] _LinkerTypes;

    public void Initialize(
        LinkerLogic linkerLogic,
        SGridCoords gridCoords,
        GameObject[] linkerTypes) {
        _LinkerLogic = linkerLogic;
        _GridCoords = gridCoords;
        _LinkerTypes = linkerTypes;
    }

    public List<LinkerObject> SpawnLinkers(int spawnCount) {
        List<LinkerObject> linkerList = new List<LinkerObject>();
        Vector3 spawnerPos = gameObject.transform.position;
        int spawnRow = _GridCoords._Row;
        for (int y = 0; y < spawnCount; ++y, ++spawnRow) {
            GameObject go = Instantiate(
                _LinkerTypes[Random.Range(0, _LinkerTypes.Length)],
                new Vector3(
                    spawnerPos.x,
                    spawnerPos.y + (Layouts._BoardTileSize.y + Layouts._BoardPadding.y) * spawnRow,
                    0
                ),
                Quaternion.identity
            );
            LinkerObject linkerObject = go.GetComponent<LinkerObject>();
            linkerObject.Initialize(_LinkerLogic);
            linkerList.Add(linkerObject);
        }
        return linkerList;
    }
}
