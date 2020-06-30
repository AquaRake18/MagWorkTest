using UnityEngine;

public class Board : MonoBehaviour {
    public Settings _Settings;
    public GameObject _BoardTilePrefab;

    void Start() {
        Vector2 boardPos = Layouts.GetBoardPos(_Settings);
        int count = 0;
        for (int y = 0; y < _Settings._BoardHeight; ++y) {
            for (int x = 0; x < _Settings._BoardWidth; ++x) {
                GameObject obj = Instantiate(
                    _BoardTilePrefab,
                    new Vector3(
                        boardPos.x + x * Layouts._BoardTileSize.x,
                        boardPos.y + y * Layouts._BoardTileSize.y,
                        0
                    ),
                    Quaternion.identity
                );
                obj.name = "Tile_" + count;
                obj.transform.parent = gameObject.transform;
                ++count;
            }
        }
    }
}
