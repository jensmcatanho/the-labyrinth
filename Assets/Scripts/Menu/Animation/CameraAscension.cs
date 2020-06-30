using System.Collections;
using UnityEngine;

namespace Menu.Animation {

    public class CameraAscension : MonoBehaviour {

        #region private fields
        [SerializeField] private float _delay;

        [SerializeField] private float _duration;

        [SerializeField] private float _height;

        private Hashtable _args;
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
        }

        private void Start() {
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