using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core {

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

        #region private variables
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

            _spawnedObjects[reference].Remove(instance);

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
                var instantiatedObject = OnInstatiationCompleted(data, asyncOperationHandle);
                NotifyInstatiation(data.Reference, instantiatedObject);
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

        private void NotifyInstatiation(AssetReference reference, GameObject instantiatedObject) {
            EventManager.Instance.QueueEvent(new Events.InstantiationCompleted(reference, instantiatedObject));
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

    namespace Events {

        public class InstantiationCompleted : GameEvent {

            public InstantiationCompleted(AssetReference reference, GameObject objectInstantiated) {
                Reference = reference;
                GameObject = objectInstantiated;
            }

            public AssetReference Reference {
                get;
            }

            public GameObject GameObject {
                get;
            }
        }

    }

    public class AssetReferenceData {

        private Vector3 _position;

        private Quaternion _rotation;

        private Transform _parent;
        
        public AssetReferenceData(AssetReference reference, Vector3 position, Quaternion rotation, Transform parent = null, string name = "") {
            Reference = reference;
            _position = position;
            _rotation = rotation;
            _parent = parent;
            Name = name;
        }

        public AssetReference Reference {
            get;
        }

        public string Name {
            get;
        }

        public AsyncOperationHandle<GameObject> InstantiateAsync() {
            return Reference.InstantiateAsync(_position, _rotation, _parent);
        }
    }

}