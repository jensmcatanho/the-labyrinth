using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Labyrinth.Menu.Camera.Animation {

    public class FadeIn : MonoBehaviour, Core.IAnimation {

        #region private fields
        [SerializeField] private Image _fadeInOverlay;

        [SerializeField] private float _delay;

        [SerializeField] private float _duration;
        #endregion

        #region public methods
        public void Play() {
            _fadeInOverlay.DOFade(0.0f, _duration)
                .SetDelay(_delay)
                .OnComplete(() => {
                    Destroy(_fadeInOverlay.gameObject);
                    Destroy(this);
                });
        }
        #endregion
    }

}