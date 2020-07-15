using System.Collections.Generic;
using UnityEngine;

public class Board
    : MonoBehaviour
    , IObserverLevelEnd {
    public int _FPS = 60;
    public float _FallSpeed = 2.4f;
    public GameObject _BoardTilePrefab;
    public GameObject _LinkerSpawnerPrefab;
    public GameObject _LinkerPrefab;

    private LinkerLogic _LinkerLogic;

    private bool _IsPostGame;

    // IObserverLevelEnd
    public void OnLevelEnd() {
        _IsPostGame = true;
    }

    void Awake() {
        Application.targetFrameRate = _FPS;
        Publisher.Instance.Subscribe(this);
        LevelSettings.Instance.Initialize();
        LevelProgress.Reset();
        ScoreConfig.Instance.Initialize();
        _IsPostGame = false;

        ObjectPooler.Pool poolBoardTiles = new ObjectPooler.Pool();
        poolBoardTiles._Count = LevelSettings.Instance._MaxWidth * LevelSettings.Instance._MaxHeight;
        poolBoardTiles._Prefab = _BoardTilePrefab;
        poolBoardTiles._Tag = ObjectPoolTypes.PoolTypeBoardTile;
        ObjectPooler.Instance.AddPool(poolBoardTiles);

        ObjectPooler.Pool poolSpawners = new ObjectPooler.Pool();
        poolSpawners._Count = LevelSettings.Instance._MaxWidth;
        poolSpawners._Prefab = _LinkerSpawnerPrefab;
        poolSpawners._Tag = ObjectPoolTypes.PoolTypeSpawner;
        ObjectPooler.Instance.AddPool(poolSpawners);

        ObjectPooler.Pool poolLinkers = new ObjectPooler.Pool();
        poolLinkers._Count = 2 * (LevelSettings.Instance._MaxWidth * LevelSettings.Instance._MaxHeight);
        poolLinkers._Prefab = _LinkerPrefab;
        poolLinkers._Tag = ObjectPoolTypes.PoolTypeLinker;
        ObjectPooler.Instance.AddPool(poolLinkers);
    }

    void Start() {
        gameObject.transform.position += Layouts.GetBoardPos();
        Layouts.UpdateCameraZoom();

        BoardTile[,] boardTiles = SpawnBackgroundTiles();

        _LinkerLogic = new LinkerLogic(
            gameObject.transform.position,
            new SGridCoords(LevelSettings.Instance.BoardWidth, LevelSettings.Instance.BoardHeight),
            boardTiles,
            _FallSpeed
        );
        _LinkerLogic.SetSpawners(SpawnLinkerSpawners());
        _LinkerLogic.Initialize();
    }

    void Update() {
        if (_IsPostGame) {
            return;
        }
        _LinkerLogic.Update();
    }

    private BoardTile[,] SpawnBackgroundTiles() {
        BoardTile[,] boardTiles = new BoardTile[LevelSettings.Instance.BoardWidth, LevelSettings.Instance.BoardHeight];
        int tileCount = 0;
        int drawRow = LevelSettings.Instance.BoardHeight - 1;
        for (int y = 0; y < LevelSettings.Instance.BoardHeight; ++y, --drawRow) {
            for (int x = 0; x < LevelSettings.Instance.BoardWidth; ++x) {
                GameObject goTile = ObjectPooler.Instance.SpawnFromPool(ObjectPoolTypes.PoolTypeBoardTile);
                goTile.transform.position = new Vector3(
                    gameObject.transform.position.x + x * (Layouts._BoardTileSize.x + Layouts._BoardPadding.x),
                    gameObject.transform.position.y + drawRow * (Layouts._BoardTileSize.y + Layouts._BoardPadding.y),
                    0
                );
                goTile.name = "Tile_" + tileCount;
                goTile.transform.parent = gameObject.transform;
                boardTiles[x,y] = goTile.GetComponent<BoardTile>();
                ++tileCount;
            }
        }
        return boardTiles;
    }

    private Dictionary<int, LinkerSpawner> SpawnLinkerSpawners() {
        Dictionary<int, LinkerSpawner> linkerSpawners = new Dictionary<int, LinkerSpawner>();
        for (int spawnerColumn = 0; spawnerColumn < LevelSettings.Instance.BoardWidth; ++spawnerColumn) {
            GameObject go = ObjectPooler.Instance.SpawnFromPool(ObjectPoolTypes.PoolTypeSpawner);
            go.transform.position = new Vector3(
                gameObject.transform.position.x + spawnerColumn * (Layouts._BoardTileSize.x + Layouts._BoardPadding.x),
                gameObject.transform.position.y + LevelSettings.Instance.BoardHeight * (Layouts._BoardTileSize.y + Layouts._BoardPadding.y),
                0
            );
            go.name = "Spawner_" + spawnerColumn;
            go.transform.parent = gameObject.transform;
            LinkerSpawner spawner = go.GetComponent<LinkerSpawner>();
            spawner.Initialize(
                _LinkerLogic,
                new SGridCoords(spawnerColumn, 0)
            );
            linkerSpawners.Add(spawnerColumn, spawner);
        }
        return linkerSpawners;
    }
}
