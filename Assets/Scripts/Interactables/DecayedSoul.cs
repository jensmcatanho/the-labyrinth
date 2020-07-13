using System.Collections;
using UnityEngine;

namespace Interactables {

    public class DecayedSoul : MonoBehaviour {

        private Shader _dissolveShader;

        [SerializeField] private float _noiseScale;

        [SerializeField] private float _duration;

        private Hashtable _args;

        private Renderer _renderer;

        public void Respect() {
            _renderer.material.shader = _dissolveShader;
            _renderer.material.SetFloat("_NoiseScale", _noiseScale);

            iTween.ValueTo(gameObject, _args);
        }

        private void Awake() {
            TryGetComponent(out _renderer);
            _dissolveShader = Shader.Find("Shader Graphs/Dissolve");

            SetUpAnimationArgs();
        }

        private void SetUpAnimationArgs() {
            _args = new Hashtable {
                { "from", 0.0f },
                { "to", 1.0f },
                { "time", _duration },
                { "onupdate", "FadeOut" },
                { "oncomplete", "DestroyObject" }
            };
        }

        private void FadeOut(float alphaThreshold) {
            _renderer.material.SetFloat("_AlphaThreshold", alphaThreshold);
        }

        private void DestroyObject() {
            Destroy(gameObject);
        }
    }

}