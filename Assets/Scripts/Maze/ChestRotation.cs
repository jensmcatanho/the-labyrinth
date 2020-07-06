using UnityEngine;

namespace Maze {

    public class ChestRotation {

        static public Quaternion Left { get; } = Quaternion.Euler(0f, 180f, 0f);

        static public Quaternion Down { get; } = Quaternion.Euler(0f, 90f, 0f);

        static public Quaternion Up { get; } = Quaternion.Euler(90f, 0f, 0f);

        static public Quaternion Right { get; } = Quaternion.identity;

    }

}
