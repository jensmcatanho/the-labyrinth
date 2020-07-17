using Menu.Animation;
using UnityEngine;

namespace Menu {

    public class CameraListener : MonoBehaviour, Core.IEventListener {

        #region public methods
        public void AddListeners() {
            Core.EventManager.Instance.AddListenerOnce<Events.MazeInstanced>(OnMazeInstanced);
            Core.EventManager.Instance.AddListenerOnce<Events.MenuCameraAscensionStarted>(OnCameraAscensionStarted);
            Core.EventManager.Instance.AddListenerOnce<Events.MenuCameraPositioned>(OnCameraPositioned);
        }

        public void RemoveListeners() {
            return;
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

        private void OnCameraAscensionStarted(Events.MenuCameraAscensionStarted e) {
            var animation = GetComponentInChildren<CameraFadeIn>();
            animation.Play();
        }

        private void OnCameraPositioned(Events.MenuCameraPositioned e) {
            var animation = GetComponentInChildren<CameraIdle>();
            animation.Play();
        }
        #endregion
    }

}
