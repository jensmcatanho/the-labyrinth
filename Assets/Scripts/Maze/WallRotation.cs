using UnityEngine;

namespace Labyrinth.Maze {

    public class WallRotation {

        static public Quaternion Left { get; } = Quaternion.Euler(90f, 90f, 0f);

        static public Quaternion Down { get; } = Quaternion.Euler(90f, 0f, 0f);

        static public Quaternion Up { get; } = Quaternion.Euler(90f, 0f, 180f);

        static public Quaternion Right { get; } = Quaternion.Euler(90f, -90f, 0f);
    }

}