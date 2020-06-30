using UnityEngine;

namespace Events {

    public class MenuCameraPositioned : GameEvent {

        public Vector3 Position {
            get;
        }

        public MenuCameraPositioned(Vector3 position) {
            Position = position;
        }
    }

}