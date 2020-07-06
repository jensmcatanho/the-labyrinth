using UnityEngine;
using UnityEngine.SceneManagement;

using Maze;

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

        #region private fields
        [SerializeField] private MazeSettings _mazeSettings;
        [SerializeField] private MazeSettings _menuSettings;
        #endregion

        #region public methods
        public void AddListeners() {
            EventManager.Instance.AddListener<Events.StartButtonClicked>(LoadGame);
            EventManager.Instance.AddListener<Events.MazeFinished>(LoadMenu);
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

        private void LoadGame(Events.StartButtonClicked e) {
            SceneManager.LoadScene((int)GameScene.Game);
        }

        private void LoadMenu(Events.MazeFinished e) {
            SceneManager.LoadScene((int)GameScene.Menu);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            GameScene sceneIndex = (GameScene)scene.buildIndex;

            switch (sceneIndex) {
                case GameScene.Menu:
                    Cursor.visible = true;
                    EventManager.Instance.QueueEvent(new Events.MenuSceneLoaded(_menuSettings));
                    break;

                case GameScene.Game:
                    EventManager.Instance.QueueEvent(new Events.GameSceneLoaded(_mazeSettings));
                    break;

                default:
                    Debug.LogError("Invalid scene build index");
                    break;
            }
        }
        #endregion
    }

}