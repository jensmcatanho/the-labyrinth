using DG.Tweening;
using UnityEngine;

namespace Labyrinth.Menu.Animation {

    public class CameraGoToSettings : MonoBehaviour, IAnimation {

        #region private fields
        [SerializeField] private Vector3 _rotation;

        [SerializeField] private float _duration;

        [SerializeField] private Ease _easing;
        #endregion

        #region public methods
        public void Play() {
            var parent = transform.parent;

            parent.DORotate(_rotation, _duration)
                .SetEase(_easing)
                .OnComplete(() => {
                    Core.EventManager.Instance.TriggerEvent(new Events.Menu.CameraMoved(MenuState.Settings));
                });
        }
        #endregion
    }

}