using Events;
using UnityEngine;

public class FinishTrigger : MonoBehaviour {

    #region private methods
    private void OnTriggerEnter(Collider other) {
        EventManager.Instance.QueueEvent(new MazeFinished());
    }
    #endregion
}

namespace Events {

    public class MazeFinished : GameEvent {
        public MazeFinished() {
        }
    }

}
