using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Events {

    public class ObjectDestroyed : GameEvent {
     
        public AssetReference AssetReference {
            get;
        }

        public GameObject GameObject {
            get;
        }

        public ObjectDestroyed(AssetReference assetReference, GameObject gameObject) {
            AssetReference = assetReference;
            GameObject = gameObject;
        }

    }

}