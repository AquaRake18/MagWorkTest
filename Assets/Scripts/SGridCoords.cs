public readonly struct SGridCoords {
    public int _Column { get; }
    public int _Row { get; }

    public SGridCoords(int column, int row) {
        _Column = column;
        _Row = row;
    }

    public override string ToString() => $"SGridCoords({_Column}, {_Row})";

    public bool IsAdjacent(SGridCoords other) {
        if (other._Column == _Column - 1
            && other._Row == _Row - 1) {
            // NorthWest
            return true;
        } else if (other._Column == _Column
            && other._Row == _Row - 1) {
            // North
            return true;
        } else if (other._Column == _Column + 1
            && other._Row == _Row - 1) {
            // NorthEast
            return true;
        } else if (other._Column == _Column + 1
            && other._Row == _Row) {
            // East
            return true;
        } else if (other._Column == _Column + 1
            && other._Row == _Row + 1) {
            // SouthEast
            return true;
        } else if (other._Column == _Column
            && other._Row == _Row + 1) {
            // South
            return true;
        } else if (other._Column == _Column - 1
            && other._Row == _Row + 1) {
            // SouthWest
            return true;
        } else if (other._Column == _Column - 1
            && other._Row == _Row) {
            // West
            return true;
        }
        return false;
    }
}
