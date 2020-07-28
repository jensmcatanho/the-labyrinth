using UnityEngine;

public static class ResolutionExtensions {

    public static bool IsEqualTo(this Resolution resolution, Resolution resolution2) {
        return resolution.width == resolution2.width && resolution.height == resolution2.height;
    }

}
