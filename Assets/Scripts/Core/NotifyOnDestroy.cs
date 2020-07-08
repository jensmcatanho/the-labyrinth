using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core {

    public class NotifyOnDestroy : MonoBehaviour {

        public AssetReference AssetReference {
            get;
            set;
        }

        private void OnDestroy() {
            if (AssetLoader.Instance)
                AssetLoader.Instance.Remove(AssetReference, gameObject);
        }
    }

}
