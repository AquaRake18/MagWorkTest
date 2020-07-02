using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour {
    public LevelSettings _Settings;
    public float _FallSpeed = 2.4f;
    public GameObject _BoardTilePrefab;
    public GameObject _LinkerSpawnerPrefab;
    public GameObject[] _LinkerTypes;

    private LinkerLogic _LinkerLogic;

    void Awake() {
        _LinkerTypes = _LinkerTypes.Take(Mathf.Clamp(_Settings._LinkerColors, 0, _LinkerTypes.Length)).ToArray();
    }

    void Start() {
        Debug.Log(_LinkerTypes.Length);
        PositionBoard();
        BoardTile[,] boardTiles = InstantiateBackgroundTiles();

        _LinkerLogic = new LinkerLogic(
            gameObject.transform.position,
            new SGridCoords(_Settings._BoardWidth, _Settings._BoardHeight),
            boardTiles,
            _FallSpeed
        );
        Dictionary<int, LinkerSpawner> linkerSpawners = InstantiateLinkerSpawners();
        _LinkerLogic.SetSpawners(linkerSpawners);
        _LinkerLogic.Start();
    }

    void Update() {
        _LinkerLogic.Update();
    }

    private void PositionBoard() {
        gameObject.transform.position = Layouts.GetBoardPos(_Settings);
    }

    private BoardTile[,] InstantiateBackgroundTiles() {
        BoardTile[,] boardTiles = new BoardTile[_Settings._BoardWidth, _Settings._BoardHeight];
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
            }
        }
        return boardTiles;
    }

    private Dictionary<int, LinkerSpawner> InstantiateLinkerSpawners() {
        Dictionary<int, LinkerSpawner> linkerSpawners = new Dictionary<int, LinkerSpawner>();
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
            spawner.Initialize(
                _LinkerLogic,
                new SGridCoords(spawnerColumn, -1),
                _LinkerTypes
            );
            linkerSpawners.Add(spawnerColumn, spawner);
        }
        return linkerSpawners;
    }
}
