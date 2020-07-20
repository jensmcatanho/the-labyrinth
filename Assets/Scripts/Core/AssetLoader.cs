using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Labyrinth.Core {

    public class AssetLoader : MonoBehaviour {

        #region singleton
        static AssetLoader _instance = null;

        public static AssetLoader Instance {
            get {
                if (_instance == null)
                    _instance = FindObjectOfType(typeof(AssetLoader)) as AssetLoader;

                return _instance;
            }
        }
        #endregion

        #region private fields
        private readonly Dictionary<AssetReference, List<GameObject>> _spawnedObjects = new Dictionary<AssetReference, List<GameObject>>();

        private readonly Dictionary<AssetReference, AsyncOperationHandle<GameObject>> _asyncOperationHandles = new Dictionary<AssetReference, AsyncOperationHandle<GameObject>>();

        private readonly Dictionary<AssetReference, Queue<AssetReferenceData>> _queuedInstantiations = new Dictionary<AssetReference, Queue<AssetReferenceData>>();
        #endregion

        #region public methods
        public void Spawn(AssetReferenceData data) {
            if (!data.Reference.RuntimeKeyIsValid())
                return;

            if (_asyncOperationHandles.ContainsKey(data.Reference)) {
                if (_asyncOperationHandles[data.Reference].IsDone)
                    SpawnFromLoadedReference(data);
                else
                    EnqueueInstantiation(data);

                return;
            }

            LoadAndSpawn(data);
        }

        public void Remove(AssetReference reference, GameObject instance) {
            Addressables.ReleaseInstance(instance);

            if (_spawnedObjects.ContainsKey(reference))
                _spawnedObjects[reference]?.Remove(instance);

            if (_spawnedObjects[reference].Count == 0) {
                if (_asyncOperationHandles[reference].IsValid())
                    Addressables.Release(_asyncOperationHandles[reference]);

                _asyncOperationHandles.Remove(reference);
            }
        }
        #endregion

        #region private methods
        private void Awake() {
            if (_instance == null) {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void LoadAndSpawn(AssetReferenceData data) {
            var operationHandle = Addressables.LoadAssetAsync<GameObject>(data.Reference);
            _asyncOperationHandles[data.Reference] = operationHandle;

            operationHandle.Completed += (operation) => {
                SpawnFromLoadedReference(data);
                SpawnFromQueue(data);
            };
        }

        private void SpawnFromLoadedReference(AssetReferenceData data) {
            data.InstantiateAsync().Completed += (asyncOperationHandle) => {
                OnInstatiationCompleted(data, asyncOperationHandle);
            };
        }

        private void EnqueueInstantiation(AssetReferenceData data) {
            if (!_queuedInstantiations.ContainsKey(data.Reference))
                _queuedInstantiations[data.Reference] = new Queue<AssetReferenceData>();

            _queuedInstantiations[data.Reference].Enqueue(data);
        }

        private void SpawnFromQueue(AssetReferenceData data) {
            if (_queuedInstantiations.ContainsKey(data.Reference)) {
                while (_queuedInstantiations[data.Reference].Any()) {
                    var queuedData = _queuedInstantiations[data.Reference].Dequeue();
                    SpawnFromLoadedReference(queuedData);
                }
            }
        }

        private GameObject OnInstatiationCompleted(AssetReferenceData data, AsyncOperationHandle<GameObject> asyncOperationHandle) {
            var instantiatedObject = asyncOperationHandle.Result;
            if (data.Name.Length > 0)
                instantiatedObject.name = data.Name;

            var notify = instantiatedObject.AddComponent<NotifyOnDestroy>();
            notify.AssetReference = data.Reference;

            if (!_spawnedObjects.ContainsKey(data.Reference))
                _spawnedObjects[data.Reference] = new List<GameObject>();

            _spawnedObjects[data.Reference].Add(instantiatedObject);
            return instantiatedObject;
        }
        #endregion
    }
}