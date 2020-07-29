using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Labyrinth.Core {

    public class DisplayManager : MonoBehaviour {

        #region singleton
        static DisplayManager _instance = null;

        public static DisplayManager Instance {
            get {
                if (_instance == null)
                    _instance = FindObjectOfType(typeof(DisplayManager)) as DisplayManager;

                return _instance;
            }
        }
        #endregion

        private readonly Dictionary<string, List<string>> _resolutionRefreshRatesMapping = new Dictionary<string, List<string>>();

        [SerializeField] private int _vSync = 0;



        #region public fields
        public bool Fullscreen {
            get {
                return Screen.fullScreen;
            }

            set {
                Screen.fullScreen = value;
            }
        }

        public List<string> AvailableResolutions {
            get {
                return _resolutionRefreshRatesMapping.Keys.ToList();
            }
        }

        public List<string> AvailableRefreshRates {
            get {
                return _resolutionRefreshRatesMapping[CurrentResolution];
            }
        }

        public string CurrentResolution {
            get {
                return ResolutionToString(Screen.currentResolution);
            }
            
            set {
                var resolutionStringSplit = string.Concat(value.Where(c => !char.IsWhiteSpace(c))).Split('x');
                var width = Convert.ToInt32(resolutionStringSplit[0]);
                var height = Convert.ToInt32(resolutionStringSplit[1]);

                Screen.SetResolution(width, height, Instance.Fullscreen);
            }
        }

        public int CurrentRefreshRate {
            get {
                return Screen.currentResolution.refreshRate;
            }

            set {
                var currentResolution = Screen.currentResolution;
                Screen.SetResolution(currentResolution.width, currentResolution.height, Instance.Fullscreen, value);
            }
        }
        
        public int VSyncCount {
            get {
                return QualitySettings.vSyncCount;
            }

            set {
                QualitySettings.vSyncCount = value;
            }
        }
        #endregion

        #region private methods
        private void Awake() {
            foreach (var resolution in Screen.resolutions) {
                var resolutionString = ResolutionToString(resolution);

                if (!_resolutionRefreshRatesMapping.ContainsKey(resolutionString)) {
                    _resolutionRefreshRatesMapping[resolutionString] = new List<string>();
                }

                _resolutionRefreshRatesMapping[resolutionString].Add(resolution.refreshRate.ToString());
            }
        }

        private void Update() {
            _vSync = QualitySettings.vSyncCount;
        }

        private string ResolutionToString(Resolution resolution) {
            var stringBuilder = new StringBuilder();
            return stringBuilder.Append(resolution.width).Append(" x ").Append(resolution.height).ToString();
        }
        #endregion
    }

}