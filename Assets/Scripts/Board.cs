using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour {
    public int _FPS = 60;
    public ScoreConfig _ScoreConfig;
    public PostGameMenu _PostGameMenu;
    public float _FallSpeed = 2.4f;
    public GameObject _BoardTilePrefab;
    public GameObject _LinkerSpawnerPrefab;
    public GameObject[] _LinkerTypes;

    private LevelSettings _Settings;
    private LinkerLogic _LinkerLogic;
    private LevelProgress _LevelProgress;
    private EndGameCondition _EndGameCondition;

    private bool _IsPostGame;

    void Awake() {
        Application.targetFrameRate = _FPS;
        _Settings = new LevelSettings();
        _LevelProgress = new LevelProgress(_Settings.Moves);
        _ScoreConfig.Initialize(_Settings, _LevelProgress);
        _EndGameCondition = new EndGameCondition(EGameMode.Score, _Settings, _LevelProgress);
        _IsPostGame = false;
    }

    void Start() {
        _LinkerTypes = _LinkerTypes.Take(Mathf.Clamp(_Settings.LinkerColors, 0, _LinkerTypes.Length)).ToArray();
        PositionBoard();
        BoardTile[,] boardTiles = InstantiateBackgroundTiles();

        _LinkerLogic = new LinkerLogic(
            _ScoreConfig,
            _EndGameCondition,
            gameObject.transform.position,
            new SGridCoords(_Settings.BoardWidth, _Settings.BoardHeight),
            boardTiles,
            _FallSpeed
        );
        Dictionary<int, LinkerSpawner> linkerSpawners = InstantiateLinkerSpawners();
        _LinkerLogic.SetSpawners(linkerSpawners);
        _LinkerLogic.Start();
    }

    void Update() {
        if (_IsPostGame) {
            return;
        }
        EndGameCondition.EGameResult gameResult = _EndGameCondition.GetGameResult();
        if (gameResult == EndGameCondition.EGameResult.Running) {
            _LinkerLogic.Update();
        } else {
            _PostGameMenu.OpenPostGameMenu(gameResult);
            _IsPostGame = true;
        }
    }

    private void PositionBoard() {
        gameObject.transform.position += Layouts.GetBoardPos(_Settings);
        Layouts.UpdateCameraZoom(_Settings);
    }

    private BoardTile[,] InstantiateBackgroundTiles() {
        BoardTile[,] boardTiles = new BoardTile[_Settings.BoardWidth, _Settings.BoardHeight];
        int tileCount = 0;
        int drawRow = _Settings.BoardHeight - 1;
        for (int y = 0; y < _Settings.BoardHeight; ++y, --drawRow) {
            for (int x = 0; x < _Settings.BoardWidth; ++x) {
                GameObject goTile = Instantiate(
                    _BoardTilePrefab,
                    new Vector3(
                        gameObject.transform.position.x + x * (Layouts._BoardTileSize.x + Layouts._BoardPadding.x),
                        gameObject.transform.position.y + drawRow * (Layouts._BoardTileSize.y + Layouts._BoardPadding.y),
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
        for (int spawnerColumn = 0; spawnerColumn < _Settings.BoardWidth; ++spawnerColumn) {
            GameObject go = Instantiate(
                _LinkerSpawnerPrefab,
                new Vector3(
                    gameObject.transform.position.x + spawnerColumn * (Layouts._BoardTileSize.x + Layouts._BoardPadding.x),
                    gameObject.transform.position.y + _Settings.BoardHeight * (Layouts._BoardTileSize.y + Layouts._BoardPadding.y),
                    0
                ),
                Quaternion.identity
            );
            go.name = "Spawner_" + spawnerColumn;
            go.transform.parent = gameObject.transform;
            LinkerSpawner spawner = go.GetComponent<LinkerSpawner>();
            spawner.Initialize(
                _LinkerLogic,
                new SGridCoords(spawnerColumn, 0),
                _LinkerTypes
            );
            linkerSpawners.Add(spawnerColumn, spawner);
        }
        return linkerSpawners;
    }
}
