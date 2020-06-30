using UnityEngine;

public enum ELinkerType : byte {
    Blue = 0,
    Green,
    Purple,
    Red,
    Yellow
}

public static class LinkerTypeUtil {
    public static Color GetColor(ELinkerType linkerType) {
        switch (linkerType) {
            case ELinkerType.Blue:
                return new Color(50, 96, 168, 255);
            case ELinkerType.Green:
                return new Color(72, 168, 50, 255);
            case ELinkerType.Purple:
                return new Color(89, 50, 168, 255);
            case ELinkerType.Red:
                return new Color(168, 52, 50, 255);
            case ELinkerType.Yellow:
                return new Color(168, 168, 50, 255);
            default:
                return new Color(255, 255, 255, 255);
        }
    }
}
