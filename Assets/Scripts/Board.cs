using UnityEngine;

public class Board : MonoBehaviour {
    public Settings _Settings;
    public GameObject _BoardTilePrefab;

    void Start() {
        PositionBoard();
        InstantiateBoardTiles();
    }

    private void PositionBoard() {
        gameObject.transform.position = Layouts.GetBoardPos(_Settings);
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
