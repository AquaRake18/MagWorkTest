public readonly struct SGridCoords {
    public int _Column { get; }
    public int _Row { get; }
    public SGridCoords(int column, int row) {
        _Column = column;
        _Row = row;
    }
    public bool IsAdjacent(int gridWidth, int gridHeight, SGridCoords other) {
        return true;
    }
}
