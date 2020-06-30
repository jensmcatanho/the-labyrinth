using Core;
using System.Collections;
using UnityEngine;

namespace Menu.Animation {

    public class CameraIdle : MonoBehaviour, IEventListener {

        [SerializeField] private float _heightDelta;

        [SerializeField] private float _duration;

        private Hashtable _args;

        public void AddListeners() {
            EventManager.Instance.AddListenerOnce<Events.MenuCameraPositioned>(StartAnimation);
        }

        public void RemoveListeners() {
            return;
        }

        private void Awake() {
            _args = new Hashtable {
                { "easetype", iTween.EaseType.easeInOutSine },
                { "looptype", iTween.LoopType.pingPong },
                { "time", _duration }
            };

            AddListeners();
        }

        private void StartAnimation(Events.MenuCameraPositioned e) {
            _args.Add("position", new Vector3(e.Position.x, e.Position.y + _heightDelta, e.Position.z));
            iTween.MoveTo(gameObject, _args);
        }
    }

}