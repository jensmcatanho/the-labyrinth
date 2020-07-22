using UnityEngine;
using UnityEngine.Audio;

namespace Labyrinth.Core {

    public class AudioManager : MonoBehaviour {

        #region singleton
        static AudioManager _instance = null;

        public static AudioManager Instance {
            get {
                if (_instance == null)
                    _instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;

                return _instance;
            }
        }
        #endregion

        #region private fields
        [SerializeField] private AudioMixer _masterMixer;
        #endregion

        #region public fields
        public float MasterVolume {
            get {
                if (_masterMixer.GetFloat("master volume", out float volume)) {
                    return MapToExternal(volume);
                }

                return 0.0f;
            }

            set {
                _masterMixer.SetFloat("master volume", MapToMixer(value));
            }
        }
        #endregion

        #region private methods
        private float MapToMixer(float volume) {
            return 0.8f * volume - 80;
        }

        private float MapToExternal(float volume) {
            return 1.25f * volume + 100;
        }
        #endregion

    }

}


