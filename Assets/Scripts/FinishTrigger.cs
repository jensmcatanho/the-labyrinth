using Events;
using UnityEngine;

public class FinishTrigger : MonoBehaviour {

    #region private methods
    private void OnTriggerEnter(Collider other) {
        Core.EventManager.Instance.QueueEvent(new MazeFinished());
    }
    #endregion
}

namespace Events {

    public class MazeFinished : Core.GameEvent {
        public MazeFinished() {
        }
    }

}
