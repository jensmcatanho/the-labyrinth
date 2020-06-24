using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, Core.IEventListener {

    [SerializeField] private GameObject _playerPrefab;

    private GameObject _playerObject;

    #region public methods
    public void AddListeners() {
       Core.EventManager.Instance.AddListenerOnce<Core.Events.GameSceneLoaded>(OnSceneLoaded);
    }

    public void RemoveListeners() {
        Core.EventManager.Instance?.RemoveListener<Core.Events.GameSceneLoaded>(OnSceneLoaded);
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

    private void OnSceneLoaded(Core.Events.GameSceneLoaded e) {
        InstantiatePlayer();
    }

    private void InstantiatePlayer() {
        _playerObject = Instantiate(_playerPrefab, new Vector3(5.0f, 1.0f, 1.0f), Quaternion.identity);
        _playerObject.transform.parent = this.transform;
    }
    #endregion
}
