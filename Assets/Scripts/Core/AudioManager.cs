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

        #region public fields
        public void SetMasterVolume(float volume) {
            _masterMixer.SetFloat("volume", volume);
        }
        #endregion

        #region private fields
        [SerializeField] private AudioMixer _masterMixer;
        #endregion


    }

}


