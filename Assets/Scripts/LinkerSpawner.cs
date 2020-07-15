using System.Collections.Generic;
using UnityEngine;

public class LinkerSpawner : MonoBehaviour {
    private LinkerLogic _LinkerLogic;
    private SGridCoords _GridCoords;
    public SGridCoords GridCoords {
        get { return _GridCoords; }
    }

    public void Initialize(
        LinkerLogic linkerLogic,
        SGridCoords gridCoords) {
        _LinkerLogic = linkerLogic;
        _GridCoords = gridCoords;
    }

    public List<LinkerObject> SpawnLinkers(int spawnCount) {
        List<LinkerObject> linkerList = new List<LinkerObject>();
        Vector3 spawnerPos = gameObject.transform.position;
        int spawnRow = _GridCoords._Row;
        for (int y = 0; y < spawnCount; ++y, ++spawnRow) {
            GameObject go = ObjectPooler.Instance.SpawnFromPool(ObjectPoolTypes.PoolTypeLinker);
            go.transform.position = new Vector3(
                spawnerPos.x,
                spawnerPos.y + (Layouts._BoardTileSize.y + Layouts._BoardPadding.y) * spawnRow,
                0
            );
            LinkerObject linkerObject = go.GetComponent<LinkerObject>();
            linkerObject.Initialize(_LinkerLogic);
            linkerList.Add(linkerObject);
        }
        return linkerList;
    }
}
