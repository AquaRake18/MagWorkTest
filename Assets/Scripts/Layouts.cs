using UnityEngine;

public static class Layouts {
    private static float _XSmall = 1.6f;
    private static float _Small = 2.25f;
    private static float _Medium = 2.87f;
    private static float _Large = 3.37f;
    private static float _XLarge = 3.92f;
    private static float _XXLarge = 4.48f;
    private static float _XXXLarge = 5.03f;

    public static Vector2 _BoardTileSize = new Vector2(.5f, .5f);
    public static Vector2 _BoardPadding = new Vector2(0f, 0f);

    public static Vector3 GetBoardPos() {
        return new Vector3(
            -((_BoardTileSize.x + _BoardPadding.x) * LevelSettings.Instance.BoardWidth) / 2f,
            -((_BoardTileSize.y + _BoardPadding.y) * LevelSettings.Instance.BoardHeight) / 2f,
            0
        );
    }

    public static void UpdateCameraZoom() {
        if (LevelSettings.Instance.BoardWidth <= 3
            && LevelSettings.Instance.BoardHeight <= 5) {
            Camera.main.orthographicSize = _XSmall;
        } else if (LevelSettings.Instance.BoardWidth <= 4
            && LevelSettings.Instance.BoardHeight <= 6) {
            Camera.main.orthographicSize = _Small;
        } else if (LevelSettings.Instance.BoardWidth <= 5
            && LevelSettings.Instance.BoardHeight <= 7) {
            Camera.main.orthographicSize = _Medium;
        } else if (LevelSettings.Instance.BoardWidth <= 6
            && LevelSettings.Instance.BoardHeight <= 8) {
            Camera.main.orthographicSize = _Large;
        } else if (LevelSettings.Instance.BoardWidth <= 7) {
            Camera.main.orthographicSize = _XLarge;
        } else if (LevelSettings.Instance.BoardWidth <= 8) {
            Camera.main.orthographicSize = _XXLarge;
        } else if (LevelSettings.Instance.BoardWidth <= 9) {
            Camera.main.orthographicSize = _XXXLarge;
        }
    }
}
