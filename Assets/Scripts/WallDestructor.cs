using UnityEngine;

namespace Labyrinth {

    public class WallDestructor : MonoBehaviour {

        private Camera _playerCamera;

        private float _maxDistance = 2.0f;

        private void Awake() {
            _playerCamera = GetComponentInChildren<Camera>();
        }

        void Update() {

            if (Input.GetKeyDown(KeyCode.F)) {
                var ray = _playerCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
                    if (hit.transform.TryGetComponent(out Wall wall))
                        Destroy(wall.gameObject);
            }
        }
    }

}

