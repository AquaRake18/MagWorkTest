public class FallLogic : AFallLogic {
    private int _GridWidth;
    private int _GridHeight;
    private BoardTile[,] _BoardTiles;
    private LinkerObject[,] _LinkerObjects;
    private float _FallSpeed;
    private bool _UnstableBoard = false;

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

    // AFallLogic
    public override void UnstableBoard() {
        _UnstableBoard = true;
    }

    public void Update(float deltaTime) {
        if (!_UnstableBoard) {
            return;
        }
    }
}
