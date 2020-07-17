using DG.Tweening;
using UnityEngine;
using Labyrinth.Menu;

namespace Menu.Animation {

    public class CameraGoToMenu : MonoBehaviour, IAnimation {

        #region private fields
        [SerializeField] private float _distance;

        [SerializeField] private float _delay;

        [SerializeField] private float _duration;

        [SerializeField] private Ease _easing;
        #endregion

        #region public methods
        public void Play() {
            var parent = transform.parent;

            parent.DOMove(new Vector3(_distance, parent.position.y, _distance), _duration)
                .SetDelay(_delay)
                .SetEase(_easing)
                .OnComplete(() => {
                    Core.EventManager.Instance.TriggerEvent(new Events.Menu.CameraMoved(MenuState.MainMenu));
                    Destroy(this);
                });
        }
        #endregion
    }

}