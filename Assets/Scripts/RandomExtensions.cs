using System;

public static class RandomExtensions {
    public static float Range(this Random random, float min = 0f, float max = 1f) {
        return (float)random.NextDouble() * (max - min) + min;
    }
}
