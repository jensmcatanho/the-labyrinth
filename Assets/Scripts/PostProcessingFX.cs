using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingFX : MonoBehaviour {

    private PostProcessVolume _ppVolume;

    private void Awake() {
        _ppVolume = GetComponent<PostProcessVolume>();
    }
}
