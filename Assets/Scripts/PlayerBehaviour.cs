using UnityEngine;

namespace Labyrinth {

    public class PlayerBehaviour : MonoBehaviour, Core.IEventListener {

        [SerializeField] private GameObject _playerPrefab;

        private GameObject _playerObject;

        #region public methods
        public void AddListeners() {
            Core.EventManager.Instance.AddListenerOnce<Events.GameSceneLoaded>(OnSceneLoaded);
        }

        public void RemoveListeners() {
            if (Core.EventManager.Instance)
                Core.EventManager.Instance.RemoveListener<Events.GameSceneLoaded>(OnSceneLoaded);
        }
        #endregion

        #region private methods
        private void Awake() {
            AddListeners();
        }

        private void OnDestroy() {
            DestroyImmediate(_playerObject);
            RemoveListeners();
        }

        private void OnSceneLoaded(Events.GameSceneLoaded e) {
            InstantiatePlayer();
        }

        private void InstantiatePlayer() {
            _playerObject = Instantiate(_playerPrefab, new Vector3(2.5f, 1.0f, 2.5f), Quaternion.identity);
            ;
            _playerObject.transform.parent = transform;
        }
        #endregion
    }

}