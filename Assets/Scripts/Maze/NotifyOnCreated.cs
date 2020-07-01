using UnityEngine;

namespace Maze {

    public class NotifyOnCreated : MonoBehaviour {

        [SerializeField] private int _allowedFrames;

        private int _childCountCurrentFrame = 0;

        void Update() {
            if (_childCountCurrentFrame == transform.childCount && --_allowedFrames == 0) {
                Core.EventManager.Instance.TriggerEvent(new Events.MazeInstanced());
                Destroy(this);
            }

            _childCountCurrentFrame = transform.childCount;
        }
    }

}

