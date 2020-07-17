using DG.Tweening;
using UnityEngine;

namespace Menu.Animation {

    public class CameraAscension : MonoBehaviour, IAnimation {

        #region private fields
        [SerializeField] private float _delay;

        [SerializeField] private float _duration;

        [SerializeField] private float _targetHeight;

        [SerializeField] private Ease _easing;
        #endregion

        #region public methods
        public void Play() {
            var parent = transform.parent;

            parent.DOMoveY(_targetHeight, _duration)
                .SetDelay(_delay)
                .SetEase(_easing)
                .OnStart(() => {
                    Core.EventManager.Instance.TriggerEvent(new Events.MenuCameraAscensionStarted());
                })
                .OnComplete(() => {
                    Core.EventManager.Instance.TriggerEvent(new Events.MenuCameraPositioned());
                    Destroy(this);
                });
        }
        #endregion
    }

}