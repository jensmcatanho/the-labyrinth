using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core {

    public class NotifyOnDestroy : MonoBehaviour {

        public AssetReference AssetReference {
            get;
            set;
        }

        private void OnDestroy() {
            if (EventManager.Instance)
                EventManager.Instance.TriggerEvent(new Events.ObjectDestroyed(AssetReference, gameObject));
        }
    }

}
