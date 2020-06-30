using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public Settings _Settings;
    public GameObject _BoardTilePrefab;
    public GameObject _LinkerSpawnerPrefab;
    private List<LinkerSpawner> _Spawners = new List<LinkerSpawner>();

    void Start() {
        PositionBoard();
        InstantiateLinkerSpawners();
        InstantiateBoardTiles();
    }

    private void PositionBoard() {
        gameObject.transform.position = Layouts.GetBoardPos(_Settings);
    }

    private void InstantiateLinkerSpawners() {
        for (int spawnerColumn = 0; spawnerColumn < _Settings._BoardWidth; ++spawnerColumn) {
            GameObject spawnerObject = Instantiate(
                _LinkerSpawnerPrefab,
                new Vector3(
                    gameObject.transform.position.x + spawnerColumn * Layouts._BoardTileSize.x,
                    gameObject.transform.position.y + _Settings._BoardHeight * Layouts._BoardTileSize.y,
                    0
                ),
                Quaternion.identity
            );
            spawnerObject.name = "Spawner_" + spawnerColumn;
            spawnerObject.transform.parent = gameObject.transform;
            LinkerSpawner spawner = spawnerObject.GetComponent<LinkerSpawner>();
            spawner._ColumnIndex = spawnerColumn;
            _Spawners.Add(spawner);
        }
    }

    private void InstantiateBoardTiles() {
        int count = 0;
        for (int y = 0; y < _Settings._BoardHeight; ++y) {
            for (int x = 0; x < _Settings._BoardWidth; ++x) {
                GameObject bgTile = Instantiate(
                    _BoardTilePrefab,
                    new Vector3(
                        gameObject.transform.position.x + x * Layouts._BoardTileSize.x,
                        gameObject.transform.position.y + y * Layouts._BoardTileSize.y,
                        0
                    ),
                    Quaternion.identity
                );
                bgTile.name = "Tile_" + count;
                bgTile.transform.parent = gameObject.transform;
                ++count;
            }
        }
    }
}
