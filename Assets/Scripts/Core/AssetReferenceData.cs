using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core {

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