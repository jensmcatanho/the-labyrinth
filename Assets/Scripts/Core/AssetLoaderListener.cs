using UnityEngine;

namespace Labyrinth.Core {

    public class AssetLoaderListener : MonoBehaviour, IEventListener {

        private AssetLoader _assetLoader;

        public void AddListeners() {
            EventManager.Instance.AddListener<Events.ObjectDestroyed>(OnObjectDestroyed);
        }

        public void RemoveListeners() {
            EventManager.Instance.RemoveListener<Events.ObjectDestroyed>(OnObjectDestroyed);
        }

        private void Awake() {
            TryGetComponent(out _assetLoader);

            AddListeners();
        }

        private void OnDestroy() {
            RemoveListeners();
        }

        private void OnObjectDestroyed(Events.ObjectDestroyed e) {
            _assetLoader.Remove(e.AssetReference, e.GameObject);
        }
    }

}