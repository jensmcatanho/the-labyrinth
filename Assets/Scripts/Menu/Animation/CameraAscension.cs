using System.Collections;
using UnityEngine;

namespace Menu.Animation {

    public class CameraAscension : MonoBehaviour, Core.IEventListener {

        #region private fields
        [SerializeField] private float _delay;

        [SerializeField] private float _duration;

        [SerializeField] private float _height;

        private Hashtable _args;
        #endregion

        #region public methods
        public void AddListeners() {
            Core.EventManager.Instance.AddListenerOnce<Events.MazeInstanced>(StartAnimation);
        }

        public void RemoveListeners() {
            return;
        }
        #endregion

        #region private methods
        private void Awake() {
            _args = new Hashtable {
                { "position", new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + _height, gameObject.transform.position.z) },
                { "easetype", iTween.EaseType.easeOutExpo },
                { "delay", _delay },
                { "time", _duration },
                { "oncomplete", "OnCameraAscensionCompleted" }
            };

            AddListeners();
        }

        private void StartAnimation(Events.MazeInstanced e) {
            Core.EventManager.Instance.TriggerEvent(new Events.MenuCameraAscensionStarted());
            iTween.MoveTo(gameObject, _args);
        }

        private void OnCameraAscensionCompleted() {
            Core.EventManager.Instance.TriggerEvent(new Events.MenuCameraPositioned(gameObject.transform.position));
            Destroy(this);
        }
        #endregion
    }

}