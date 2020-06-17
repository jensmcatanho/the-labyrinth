using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IEventListener {

    [SerializeField] private GameObject _playerPrefab;

    private GameObject _playerObject;

    #region public methods
    public void AddListeners() {
        EventManager.Instance.AddListenerOnce<Events.GameSceneLoaded>(OnSceneLoaded);
    }

    public void RemoveListeners() {
        EventManager.Instance?.RemoveListener<Events.GameSceneLoaded>(OnSceneLoaded);
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
		_playerObject = Instantiate(_playerPrefab, new Vector3 (5.0f, 1.0f, 1.0f), Quaternion.identity);
        _playerObject.transform.parent = this.transform;
    }
    #endregion
}
