using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu {

    public class CameraController : MonoBehaviour {

        private Hashtable _initialMovementArgs = null;

        private Hashtable _idleMovementArgs = null;

        private void Awake() {
            iTween.Init(gameObject);

            _initialMovementArgs = new Hashtable {
                { "position", new Vector3(115, 50, 40) },
                { "easetype", iTween.EaseType.easeOutExpo },
                { "delay", 2.0f },
                { "time", 10.0f },
                { "oncomplete", "IdleMovement" }
            };

            _idleMovementArgs = new Hashtable {
                { "position", new Vector3(115, 50.25f, 40) },
                { "easetype", iTween.EaseType.easeInOutSine },
                { "looptype", iTween.LoopType.pingPong },
                { "time", 2.0f }
            };
        }

        private void Start() {
            iTween.MoveTo(gameObject, _initialMovementArgs);
        }

        private void IdleMovement() {
            iTween.MoveTo(gameObject, _idleMovementArgs);

        }
    }
}
