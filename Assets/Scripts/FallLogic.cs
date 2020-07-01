public class FallLogic {
    private int _GridWidth;
    private int _GridHeight;
    private BoardTile[,] _BoardTiles;
    private LinkerObject[,] _LinkerObjects;
    private float _FallSpeed;

    public void Initialize(
        int gridWidth,
        int gridHeight,
        BoardTile[,] boardTiles,
        LinkerObject[,] linkerObjects,
        float fallSpeed) {
        _GridWidth = gridWidth;
        _GridHeight = gridHeight;
        _BoardTiles = boardTiles;
        _LinkerObjects = linkerObjects;
        _FallSpeed = fallSpeed;
    }

    public void Update(float deltaTime) {
    }
}
