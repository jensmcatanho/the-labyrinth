using UnityEngine;

namespace Labyrinth {

    public class FinishTrigger : MonoBehaviour {

        [SerializeField] private static float _length = 1.5f;

        [SerializeField] private static float _height = 2f;

        [SerializeField] private static float _width = 0.5f;

        #region static methods
        public static GameObject Spawn(Maze.Cell cell) {
            var finishTrigger = new GameObject("Finish Trigger");

            if (cell.Position.X > cell.Position.Y) {
                finishTrigger.transform.position = new Vector3((2 * cell.Position.X + 4) * cell.Size - 2 * cell.Size, 1.0f, (2 * cell.Position.Y + 1) * cell.Size);
                finishTrigger.transform.localScale = new Vector3(_width * cell.Size, _height * cell.Size, _length * cell.Size);

            } else {
                finishTrigger.transform.position = new Vector3((2 * cell.Position.X + 4) * cell.Size * cell.Size, 1.0f, (2 * cell.Position.Y + 1) * cell.Size - 2);
                finishTrigger.transform.localScale = new Vector3(_length * cell.Size, _height * cell.Size, _width * cell.Size);
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

}