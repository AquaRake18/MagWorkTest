using System.Collections.Generic;
using UnityEngine;

public class FallLogic {
    private Vector3 _BoardPosition;
    private SGridCoords _BoardSize;
    private BoardTile[,] _BoardTiles;
    private Dictionary<int, LinkerSpawner> _LinkerSpawners;
    private LinkerObject[,] _LinkerObjects;
    private float _FallSpeed;
    private bool _CollapsingCollumns = false;

    private readonly int _MaxShuffles = 2000;

    private readonly struct SRefillData {
        public int _Column { get; }
        public int _EmptySpaces { get; }

        public SRefillData(int column, int emptySpaces) {
            _Column = column;
            _EmptySpaces = emptySpaces;
        }
    }

    public FallLogic(
        Vector3 boardPosition,
        SGridCoords boardSize,
        BoardTile[,] boardTiles,
        float fallSpeed) {
        _BoardPosition = boardPosition;
        _BoardSize = boardSize;
        _BoardTiles = boardTiles;
        _FallSpeed = fallSpeed;
        _LinkerObjects = new LinkerObject[_BoardSize._Column, _BoardSize._Row];
    }

    public void SetSpawners(Dictionary<int, LinkerSpawner> linkerSpawners) {
        _LinkerSpawners = linkerSpawners;
    }

    public void Start() {
        List<SRefillData> refillDataList = new List<SRefillData>();
        for (int column = 0; column < _BoardSize._Column; ++column) {
            refillDataList.Add(new SRefillData(column, _BoardSize._Row));
        }
        RefillCollumns(refillDataList);
    }

    public void Update() {
        if (!_CollapsingCollumns) {
            return;
        }
        for (int x = 0; x < _BoardSize._Column; ++x) {
            for (int y = 0; y < _BoardSize._Row; ++y) {
                if (_LinkerObjects[x, y]
                    && _LinkerObjects[x, y].IsFalling()) {
                    return;
                }
            }
        }
        _CollapsingCollumns = false;

        int shuffleCount = 0;
        while (shuffleCount < _MaxShuffles
            && !BoardController.HasAnyThreeOrMoreChains(_BoardSize, _LinkerObjects)) {
            ++shuffleCount;
            if (shuffleCount < _MaxShuffles) {
                BoardController.ShuffleBoard(_BoardSize, ref _BoardTiles, ref _LinkerObjects);
            } else {
                Debug.LogError("Didn't manage to shuffle. Level is unplayable.");
            }
        }
    }

    public bool IsCollapsingCollumns() {
        return _CollapsingCollumns;
    }

    public void CollapseCollumns() {
        _CollapsingCollumns = true;
        int emptyRows = 0;
        List<SRefillData> refillDataList = new List<SRefillData>();
        for (int x = 0; x < _BoardSize._Column; ++x) {
            for (int y = _BoardSize._Row - 1; y >= 0; --y) {
                if (!_LinkerObjects[x, y]) {
                    continue;
                }
                if (_LinkerObjects[x, y].IsDestroyed()) {
                    ++emptyRows;
                } else if (emptyRows > 0) {
                    _LinkerObjects[x, y].SetFalling(
                        _FallSpeed,
                        _LinkerObjects[x, y + emptyRows].gameObject.transform.position,
                        new SGridCoords(x, y + emptyRows)
                    );
                }
            }
            if (emptyRows > 0) {
                refillDataList.Add(new SRefillData(x, emptyRows));
            }
            emptyRows = 0;
        }
        RefreshLinkersTileParents();
        RefillCollumns(refillDataList);
    }

    private void RefreshLinkersTileParents() {
        for (int x = 0; x < _BoardSize._Column; ++x) {
            for (int y = _BoardSize._Row - 1; y >= 0; --y) {
                LinkerObject obj = _LinkerObjects[x, y];
                if (obj) {
                    int destX = obj._GridCoords._Column;
                    int destY = obj._GridCoords._Row;
                    if (!(destX == x && destY == y)) {
                        _LinkerObjects[destX, destY] = obj;
                        _LinkerObjects[x, y] = null;
                        obj.gameObject.transform.parent = _BoardTiles[destX, destY].gameObject.transform;
                    }
                }
            }
        }
    }

    private void RefillCollumns(List<SRefillData> refillDataList) {
        foreach (SRefillData refillData in refillDataList) {
            LinkerSpawner spawner = _LinkerSpawners[refillData._Column];
            if (spawner) {
                List<LinkerObject> linkerObjects = spawner.SpawnLinkers(refillData._EmptySpaces);
                for (int spawnIndex = 0; spawnIndex < linkerObjects.Count; ++spawnIndex) {
                    LinkerObject linker = linkerObjects[spawnIndex];
                    SGridCoords destCoords = new SGridCoords(refillData._Column, refillData._EmptySpaces - 1 - spawnIndex);
                    linker._GridCoords = destCoords;
                    _LinkerObjects[destCoords._Column, destCoords._Row] = linker;
                    linker.gameObject.transform.parent = _BoardTiles[destCoords._Column, destCoords._Row].gameObject.transform;
                    linker.SetFalling(
                        _FallSpeed,
                        _BoardTiles[destCoords._Column, destCoords._Row].gameObject.transform.position,
                        destCoords
                    );
                }
            }
        }
    }
}
