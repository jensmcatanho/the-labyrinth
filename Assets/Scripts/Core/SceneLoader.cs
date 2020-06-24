using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core {

enum GameScene {
    Menu,
    Game
}

public class SceneLoader : MonoBehaviour, IEventListener {

    #region singleton
    static SceneLoader _instance = null;

    public static SceneLoader Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(SceneLoader)) as SceneLoader;

            return _instance;
        }
    }
    #endregion

    #region public methods
    public void AddListeners() {
        EventManager.Instance.AddListener<StartButtonClicked>(LoadGame);
        EventManager.Instance.AddListener<MazeFinished>(LoadMenu);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void RemoveListeners() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion

    #region private methods
    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);

        } else {
            DestroyImmediate(gameObject);
        }

        AddListeners();
    }

    private void OnDestroy() {
        RemoveListeners();
    }

    private void LoadGame(StartButtonClicked e) {
        SceneManager.LoadScene((int)GameScene.Game);
    }

    private void LoadMenu(MazeFinished e) {
        SceneManager.LoadScene((int)GameScene.Menu);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameScene sceneIndex = (GameScene)scene.buildIndex;

        switch (sceneIndex) {
            case GameScene.Menu:
                Cursor.visible = true;
                EventManager.Instance.QueueEvent(new Events.MenuSceneLoaded());
                break;

            case GameScene.Game:
                EventManager.Instance.QueueEvent(new Events.GameSceneLoaded());
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
        public MenuSceneLoaded() {
        }
    }

    public class GameSceneLoaded : GameEvent {
        public GameSceneLoaded() {
        }
    }

}

}