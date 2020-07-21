using Labyrinth.Menu.Animation;
using UnityEngine;

namespace Labyrinth.Menu {

    public class CameraListener : MonoBehaviour, Core.IEventListener {

        #region public methods
        public void AddListeners() {
            Core.EventManager.Instance.AddListenerOnce<Events.MazeInstanced>(OnMazeInstanced);
            Core.EventManager.Instance.AddListenerOnce<Events.Menu.CameraAscensionStarted>(OnCameraAscensionStarted);
            Core.EventManager.Instance.AddListenerOnce<Events.Menu.AnyButtonClicked>(OnAnyButtonClicked);

            Core.EventManager.Instance.AddListener<Events.Menu.CameraMoved>(OnCameraMoved);
            Core.EventManager.Instance.AddListener<Events.Menu.SettingsButtonClicked>(OnSettingsButtonClicked);
            Core.EventManager.Instance.AddListener<Events.Menu.BackToMenuClicked>(OnBackToMenuClicked);
        }

        public void RemoveListeners() {
            if (Core.EventManager.Instance) {
                Core.EventManager.Instance.RemoveListener<Events.Menu.CameraMoved>(OnCameraMoved);
                Core.EventManager.Instance.RemoveListener<Events.Menu.SettingsButtonClicked>(OnSettingsButtonClicked);
                Core.EventManager.Instance.RemoveListener<Events.Menu.BackToMenuClicked>(OnBackToMenuClicked);
            }
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
            var animation = GetComponentInChildren<CameraGoToMenuFromStartScreen>();
            animation.Play();
        }
        
        private void OnSettingsButtonClicked(Events.Menu.SettingsButtonClicked e) {
            var animation = GetComponentInChildren<CameraGoToSettings>();
            animation.Play();
        }

        private void OnBackToMenuClicked(Events.Menu.BackToMenuClicked e) {
            var animation = GetComponentInChildren<CameraGoToMenuFromSettings>();
            animation.Play();
        }
        #endregion
    }

}
