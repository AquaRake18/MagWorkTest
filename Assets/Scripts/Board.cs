using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public LevelSettings _Settings;
    public float _FallSpeed = .6f;
    public GameObject _BoardTilePrefab;
    public GameObject _LinkerSpawnerPrefab;
    public GameObject[] _LinkerTypes;

    private FallLogic _FallLogic = new FallLogic();
    private LinkerLogic _LinkerLogic;
    private List<LinkerSpawner> _Spawners = new List<LinkerSpawner>();

    void Start() {
        _LinkerLogic = new LinkerLogic(_FallLogic);
        PositionBoard();
        InstantiateLinkerSpawners();
        InstantiateTilesWithLinkers();
    }

    void Update() {
        _FallLogic.Update(Time.deltaTime);
    }

    private void PositionBoard() {
        gameObject.transform.position = Layouts.GetBoardPos(_Settings);
    }

    private void InstantiateLinkerSpawners() {
        for (int spawnerColumn = 0; spawnerColumn < _Settings._BoardWidth; ++spawnerColumn) {
            GameObject go = Instantiate(
                _LinkerSpawnerPrefab,
                new Vector3(
                    gameObject.transform.position.x + spawnerColumn * Layouts._BoardTileSize.x,
                    gameObject.transform.position.y + _Settings._BoardHeight * Layouts._BoardTileSize.y,
                    0
                ),
                Quaternion.identity
            );
            go.name = "Spawner_" + spawnerColumn;
            go.transform.parent = gameObject.transform;
            LinkerSpawner spawner = go.GetComponent<LinkerSpawner>();
            spawner._ColumnIndex = spawnerColumn;
            _Spawners.Add(spawner);
        }
    }

    private void InstantiateTilesWithLinkers() {
        BoardTile[,] boardTiles = new BoardTile[_Settings._BoardWidth, _Settings._BoardHeight];
        LinkerObject[,] linkerObjects = new LinkerObject[_Settings._BoardWidth, _Settings._BoardHeight];
        int tileCount = 0;
        int drawRow = _Settings._BoardHeight - 1;
        for (int y = 0; y < _Settings._BoardHeight; ++y, --drawRow) {
            for (int x = 0; x < _Settings._BoardWidth; ++x) {
                GameObject goTile = Instantiate(
                    _BoardTilePrefab,
                    new Vector3(
                        gameObject.transform.position.x + x * Layouts._BoardTileSize.x,
                        gameObject.transform.position.y + drawRow * Layouts._BoardTileSize.y,
                        0
                    ),
                    Quaternion.identity
                );
                goTile.name = "Tile_" + tileCount;
                goTile.transform.parent = gameObject.transform;
                boardTiles[x,y] = goTile.GetComponent<BoardTile>();
                ++tileCount;

                GameObject linkerObjectType = _LinkerTypes[Random.Range(0, Mathf.Clamp(_LinkerTypes.Length, 0, _Settings._LinkerColors))];
                GameObject goLinker = Instantiate(
                    linkerObjectType,
                    goTile.transform.position,
                    Quaternion.identity
                );
                goLinker.transform.parent = boardTiles[x,y].transform;
                linkerObjects[x,y] = goLinker.GetComponent<LinkerObject>();
                linkerObjects[x,y].Reset(_LinkerLogic, new SGridCoords(x, y));
            }
        }
        _FallLogic.Initialize(_Settings._BoardWidth, _Settings._BoardHeight, boardTiles, linkerObjects, _FallSpeed);
    }
}
