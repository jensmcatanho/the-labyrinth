using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

enum GameScene {
    Menu,
    Game
}

public class SceneLoader : MonoBehaviour, IEventListener {

    #region public methods
    public void AddListeners() {
        EventManager.Instance.AddListener<Events.StartButtonClicked>(LoadGame);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void RemoveListeners() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion

    #region private methods
    private void Awake() {
        AddListeners();
    }
    
    private void OnDestroy() {
        RemoveListeners();
    }

    private void LoadGame(Events.StartButtonClicked e) {
        SceneManager.LoadScene((int)GameScene.Game);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameScene sceneIndex = (GameScene)scene.buildIndex;

        switch (sceneIndex) {
            case GameScene.Menu:
                EventManager.Instance.QueueEvent(new MenuSceneLoaded());
                break;

            case GameScene.Game:
                EventManager.Instance.QueueEvent(new GameSceneLoaded());
                break;

            default:
                Debug.LogError("Invalid scene build index");
                break;
        }
    }
    #endregion
}

namespace Events {

    public class MenuSceneLoaded : GameEvent {
        public MenuSceneLoaded() { }
    }

    public class GameSceneLoaded : GameEvent {
        public GameSceneLoaded() { }
    }

}