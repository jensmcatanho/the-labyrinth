using Labyrinth.Menu.Animation;
using UnityEngine;

namespace Labyrinth.Menu {

    public class CameraListener : MonoBehaviour, Core.IEventListener {

        #region public methods
        public void AddListeners() {
            Core.EventManager.Instance.AddListenerOnce<Events.MazeInstanced>(OnMazeInstanced);
            Core.EventManager.Instance.AddListenerOnce<Events.Menu.CameraAscensionStarted>(OnCameraAscensionStarted);
            Core.EventManager.Instance.AddListenerOnce<Events.Menu.CameraMoved>(OnCameraMoved);
            Core.EventManager.Instance.AddListenerOnce<Events.Menu.AnyButtonClicked>(OnAnyButtonClicked);
        }

        public void RemoveListeners() {
            Core.EventManager.Instance.RemoveListener<Events.Menu.CameraMoved>(OnCameraMoved);
        }
        #endregion

        #region private methods
        private void Awake() {
            AddListeners();
        }

        private void OnMazeInstanced(Events.MazeInstanced e) {
            var animation = GetComponentInChildren<CameraAscension>();
            animation.Play();
        }

        private void OnCameraAscensionStarted(Events.Menu.CameraAscensionStarted e) {
            var animation = GetComponentInChildren<CameraFadeIn>();
            animation.Play();
        }

        private void OnCameraMoved(Events.Menu.CameraMoved e) {
            if (e.NewState == MenuState.StartScreen) {
                var animation = GetComponentInChildren<CameraIdle>();
                animation.Play();
            }
        }

        private void OnAnyButtonClicked(Events.Menu.AnyButtonClicked e) {
            var animation = GetComponentInChildren<CameraGoToMenu>();
            animation.Play();
        }
        #endregion
    }

}
