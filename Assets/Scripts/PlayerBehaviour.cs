using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IEventListener {

    [SerializeField] private GameObject _playerPrefab;

    public void AddListeners() {
        EventManager.Instance.AddListener<Events.GameSceneLoaded>(OnSceneLoaded);
    }

    public void RemoveListeners() {
        if (EventManager.Instance != null) {
            EventManager.Instance.RemoveListener<Events.GameSceneLoaded>(OnSceneLoaded);
        }
    }

    private void Awake() {
        AddListeners();
    }

    private void OnDestroy() {
        RemoveListeners();
    }

    private void OnSceneLoaded(Events.GameSceneLoaded e) {
        InstantiatePlayer();
    }

    private void InstantiatePlayer() {
		GameObject playerController = Instantiate(_playerPrefab, new Vector3 (5.0f, 1.0f, 1.0f), new Quaternion());
        playerController.transform.parent = this.transform;
    }
}
