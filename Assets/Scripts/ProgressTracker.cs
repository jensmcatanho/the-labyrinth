using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Labyrinth {

    public class ProgressTracker : MonoBehaviour, Core.IEventListener {

        [SerializeField] private GameObject _target;

        [SerializeField] private RenderTexture _currentProgressTexture;

        [SerializeField] private Material _targetMaterial;

        private Camera _camera;

        [SerializeField] private Texture2D _currentTextureBlending;

        private bool _tracking = true;

        private Queue<Texture> _accumulatedProgress = new Queue<Texture>();

        public void AddListeners() {
            Core.EventManager.Instance.AddListenerOnce<Events.GameSceneLoaded>(OnGameSceneLoaded);
            // Core.EventManager.Instance.AddListener<Events.PlayerMoved>(OnPlayerMoved);
        }

        public void RemoveListeners() {
            return;
        }

        private void Awake() {
            AddListeners();

            _camera = GetComponentInChildren<Camera>();
            _currentProgressTexture = new RenderTexture(_camera.pixelWidth, _camera.pixelHeight, 24);
            _targetMaterial.SetTexture("_MainTex", _currentProgressTexture);
            _target.GetComponent<MeshRenderer>().material = _targetMaterial;
        }

        private void FixedUpdate() {
            if (_tracking) {
                _camera.Render();
                _accumulatedProgress.Enqueue(_currentProgressTexture);
            }

            if (Input.GetKeyDown(KeyCode.P)) {
                UpdateTexture();

                _targetMaterial.SetTexture("_MainTex", _currentTextureBlending);
                _target.GetComponent<MeshRenderer>().material = _targetMaterial;
            }
        }

        private void UpdateTexture() {
            if (_currentTextureBlending == null) {
                var nextTexture = _accumulatedProgress.Dequeue() as RenderTexture;
                _currentTextureBlending = nextTexture.ToTexture2D();
            }

            if (_accumulatedProgress.Any()) {
                var nextTexture = _accumulatedProgress.Dequeue() as RenderTexture;
                _currentTextureBlending = _currentTextureBlending.Blend(nextTexture.ToTexture2D());
            }
        }

        private void OnGameSceneLoaded(Events.GameSceneLoaded e) {
            SetupCamera(e.MazeSettings);
        }

        private void SetupCamera(Maze.MazeSettings mazeSettings) {
            int length = mazeSettings.Length;
            int width = mazeSettings.Width;

            _camera.transform.position = new Vector3(2 * length, _camera.transform.position.y, 2 * width);
            _camera.orthographicSize = length > width ? 2.1f * length : 2.1f * width;
            _camera.targetTexture = _currentProgressTexture;
        }
    }

}