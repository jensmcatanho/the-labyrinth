using DG.Tweening;
using UnityEngine;

namespace Labyrinth.Menu.Camera.Animation {

    public class Idle : MonoBehaviour, Core.IAnimation {

        #region private fields
        [SerializeField] private float _heightFluctuation;

        [SerializeField] private float _duration;

        [SerializeField] private int _loopCount;

        [SerializeField] private Ease _easing;
        #endregion

        #region public methods
        public void Play() {
            var parent = transform.parent;
            parent.DOMoveY(parent.position.y + _heightFluctuation, _duration)
                .SetEase(_easing)
                .SetLoops(_loopCount, LoopType.Yoyo);
        }
        #endregion

    }
}