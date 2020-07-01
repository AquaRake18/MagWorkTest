public enum EDirection {
    East,
    NorthEast,
    North,
    NorthWest,
    West,
    SouthWest,
    South,
    SouthEast
}

public static class Direction {
    public static EDirection AngleToDirection(float angle) {
        if (angle > -22.5f && angle <= 22.5f) {
            return EDirection.East;
        } else if (angle > 22.5f && angle <= 67.5f) {
            return EDirection.NorthEast;
        } else if (angle > 67.5f && angle <= 112.5f) {
            return EDirection.North;
        } else if (angle > 112.5f && angle <= 157.5f) {
            return EDirection.NorthWest;
        } else if (angle > 157.5f || angle < -157.5f) {
            return EDirection.West;
        } else if (angle < -112.5f && angle >= -157.5f) {
            return EDirection.SouthWest;
        } else if (angle < -67.5f && angle >= -112.5f) {
            return EDirection.South;
        } else {
            //angle < -22.5f && angle >= -67.5f
            return EDirection.SouthEast;
        }
    }
}
