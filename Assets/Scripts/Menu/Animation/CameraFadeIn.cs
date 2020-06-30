using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Animation {

    public class CameraFadeIn : MonoBehaviour, Core.IEventListener {

        #region private fields
        [SerializeField] private Image _fadeInImage;

        [SerializeField] private float _delay;

        [SerializeField] private float _duration;

        private Hashtable _args;
        #endregion

        #region public methods
        public void AddListeners() {
            Core.EventManager.Instance.AddListenerOnce<Events.MenuCameraAscensionStarted>(OnCameraAscensionStarted);
        }

        public void RemoveListeners() {
            return;
        }
        #endregion

        #region private methods
        private void Awake() {
            _fadeInImage.gameObject.SetActive(true);
            _fadeInImage.canvasRenderer.SetColor(Color.black);
            _fadeInImage.canvasRenderer.SetAlpha(1.0f);

            _args = new Hashtable {
                { "from", 1.0f },
                { "to", 0.0f },
                { "delay", _delay },
                { "time", _duration },
                { "onupdate", "FadeIn" }
            };

            AddListeners();
        }

        private void Update() {
            if (_fadeInImage.canvasRenderer.GetAlpha() == 0.0f) {
                Destroy(_fadeInImage.gameObject);
                Destroy(this);
            }
        }

        private void OnCameraAscensionStarted(Events.MenuCameraAscensionStarted e) {
            iTween.ValueTo(gameObject, _args);
        }

        private void FadeIn(float alpha) {
            _fadeInImage.canvasRenderer.SetAlpha(alpha);
        }
        #endregion
    }

}