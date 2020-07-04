using UnityEngine;

public static class Layouts {
    private static float _XSmall = 1.6f;
    private static float _Small = 1.9f;
    private static float _Medium = 2.36f;
    private static float _Large = 2.76f;
    private static float _XLarge = 3.24f;
    private static float _XXLarge = 3.65f;
    private static float _XXXLarge = 4.1f;

    public static Vector2 _BoardTileSize = new Vector2(.5f, .5f);
    public static Vector2 _BoardPadding = new Vector2(0f, 0f);

    public static Vector3 GetBoardPos(LevelSettings settings) {
        return new Vector3(
            -((_BoardTileSize.x + _BoardPadding.x) * settings.BoardWidth) / 2f,
            -((_BoardTileSize.y + _BoardPadding.y) * settings.BoardHeight) / 2f,
            0
        );
    }

    public static void UpdateCameraZoom(LevelSettings settings) {
        if (settings.BoardWidth <= 3
            && settings.BoardHeight <= 5) {
            Camera.main.orthographicSize = _XSmall;
        } else if (settings.BoardWidth <= 4
            && settings.BoardHeight <= 6) {
            Camera.main.orthographicSize = _Small;
        } else if (settings.BoardWidth <= 5
            && settings.BoardHeight <= 7) {
            Camera.main.orthographicSize = _Medium;
        } else if (settings.BoardWidth <= 6
            && settings.BoardHeight <= 8) {
            Camera.main.orthographicSize = _Large;
        } else if (settings.BoardWidth <= 7) {
            Camera.main.orthographicSize = _XLarge;
        } else if (settings.BoardWidth <= 8) {
            Camera.main.orthographicSize = _XXLarge;
        } else if (settings.BoardWidth <= 9) {
            Camera.main.orthographicSize = _XXXLarge;
        }
    }
}
