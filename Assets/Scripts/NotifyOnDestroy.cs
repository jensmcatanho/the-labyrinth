using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core {

public class NotifyOnDestroy : MonoBehaviour {

    public AssetReference AssetReference {
        get;
        set;
    }

    private void OnDestroy() {
        Core.EventManager.Instance.QueueEvent(new Events.InstanceDestroyed(AssetReference, gameObject));
    }
}

namespace Events {

    public class InstanceDestroyed : Core.GameEvent {

        public InstanceDestroyed(AssetReference reference, GameObject objectDestroyed) {
            Reference = reference;
            GameObject = objectDestroyed;
        }

        public AssetReference Reference {
            get;
        }

        public GameObject GameObject {
            get;
        }
    }

}

}