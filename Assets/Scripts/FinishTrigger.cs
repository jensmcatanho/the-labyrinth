using Maze;
using UnityEngine;

public class FinishTrigger : MonoBehaviour {

    #region public methods
    public static GameObject Spawn(Cell cell) {
        var finishTrigger = new GameObject("Finish Trigger");

        if (cell.Position.X > cell.Position.Y) {
            finishTrigger.transform.position = new Vector3((2 * cell.Position.X + 4) * cell.Size - 2 * cell.Size, 1.0f, (2 * cell.Position.Y + 1) * cell.Size);
            finishTrigger.transform.localScale = new Vector3(.5f * cell.Size, 2f * cell.Size, 1.5f * cell.Size);

        } else {
            finishTrigger.transform.position = new Vector3((2 * cell.Position.X + 4) * cell.Size * cell.Size, 1.0f, (2 * cell.Position.Y + 1) * cell.Size - 2);
            finishTrigger.transform.localScale = new Vector3(1.5f * cell.Size, 2f * cell.Size, .5f * cell.Size);
        }

        finishTrigger.AddComponent<FinishTrigger>();
        finishTrigger.AddComponent<BoxCollider>().isTrigger = true;

        return finishTrigger;
    }
    #endregion

    #region private methods
    private void OnTriggerEnter(Collider other) {
        Core.EventManager.Instance.QueueEvent(new Events.MazeFinished());
    }
    #endregion
}