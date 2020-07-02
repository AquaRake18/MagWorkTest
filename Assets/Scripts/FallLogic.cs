public class FallLogic : AFallLogic {
    private int _GridWidth;
    private int _GridHeight;
    private BoardTile[,] _BoardTiles;
    private LinkerObject[,] _LinkerObjects;
    private float _FallSpeed;
    private bool _CollapsingCollumns = false;

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
    public override bool IsCollapsingCollumns() {
        return _CollapsingCollumns;
    }

    // AFallLogic
    public override void CollapseCollumns() {
        _CollapsingCollumns = true;
        int emptyRows = 0;
        for (int x = 0; x < _GridWidth; ++x) {
            for (int y = _GridHeight - 1; y >= 0; --y) {
                if (!_LinkerObjects[x, y]) {
                    continue;
                }
                if (_LinkerObjects[x, y].IsDestroyed()) {
                    ++emptyRows;
                } else if (emptyRows > 0) {
                    _LinkerObjects[x, y].SetFalling(
                        _FallSpeed,
                        _LinkerObjects[x, y + emptyRows].gameObject.transform.position,
                        new SGridCoords(x, y + emptyRows)
                    );
                }
            }
            emptyRows = 0;
        }
        RefreshArrays();
    }

    private void RefreshArrays() {
        for (int x = 0; x < _GridWidth; ++x) {
            for (int y = _GridHeight - 1; y >= 0; --y) {
                LinkerObject obj = _LinkerObjects[x, y];
                if (obj
                    && !(obj._GridCoords._Column == x
                    && obj._GridCoords._Row == y)) {
                    _LinkerObjects[obj._GridCoords._Column, obj._GridCoords._Row] = obj;
                    _LinkerObjects[x, y] = null;
                }
            }
        }
    }

    public void Update() {
        if (!_CollapsingCollumns) {
            return;
        }
        for (int x = 0; x < _GridWidth; ++x) {
            for (int y = 0; y < _GridHeight; ++y) {
                if (_LinkerObjects[x, y]
                    && _LinkerObjects[x, y].IsFalling()) {
                    return;
                }
            }
        }
        _CollapsingCollumns = false;
    }
}
