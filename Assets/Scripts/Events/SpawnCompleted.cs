using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Events {

    public class SpawnCompleted : GameEvent {

        public SpawnCompleted(AssetReference reference, GameObject objectInstantiated) {
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