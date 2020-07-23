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

        [SerializeField] private string _masterAttenuation;

        [SerializeField] private string _soundFXAttenuation;
        
        [SerializeField] private string _musicAttenuation;
        
        [SerializeField] private string _ambienceAttenuation;
        #endregion

        #region public fields
        public float MasterVolume {
            get {
                return GetAttenuation(_masterAttenuation);
            }

            set {
                SetAttenuation(_masterAttenuation, value);
            }
        }

        public float SoundFXVolume {
            get {
                return GetAttenuation(_soundFXAttenuation);
            }

            set {
                SetAttenuation(_soundFXAttenuation, value);
            }
        }

        public float MusicVolume {
            get {
                return GetAttenuation(_musicAttenuation);
            }

            set {
                SetAttenuation(_musicAttenuation, value);
            }
        }

        public float AmbienceVolume {
            get {
                return GetAttenuation(_ambienceAttenuation);
            }

            set {
                SetAttenuation(_ambienceAttenuation, value);
            }
        }
        #endregion

        #region private methods
        private float GetAttenuation(string target) {
            if (_masterMixer.GetFloat(target, out float volume)) {
                return MapToExternal(volume);
            }

            return 0.0f;
        }

        private void SetAttenuation(string target, float value) {
                _masterMixer.SetFloat(target, MapToMixer(value));
        }

        private float MapToMixer(float volume) {
            return 0.8f * volume - 80;
        }

        private float MapToExternal(float volume) {
            return 1.25f * volume + 100;
        }
        #endregion

    }

}


