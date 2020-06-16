using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

enum GameScene {
    Menu,
    Game
}

public class SceneLoader : MonoBehaviour, IEventListener {

    public void AddListeners() {
        EventManager.Instance.AddListener<Events.StartButtonClicked>(LoadGame);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void RemoveListeners() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

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
}

namespace Events {

    public class MenuSceneLoaded : GameEvent {
        public MenuSceneLoaded() { }
    }

    public class GameSceneLoaded : GameEvent {
        public GameSceneLoaded() { }
    }

}