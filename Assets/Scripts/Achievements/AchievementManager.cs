using UnityEngine;
using System.Collections.Generic;


#if !DISABLESTEAMWORKS
using Steamworks;
#endif

namespace Achievements {

    public class AchievementManager : MonoBehaviour {

        #region singleton
        static AchievementManager _instance = null;

        public static AchievementManager Instance {
            get {
                if (_instance == null)
                    _instance = FindObjectOfType(typeof(AchievementManager)) as AchievementManager;

                return _instance;
            }
        }
        #endregion

        #region private fields
        [SerializeReference] private List<Achievement> _achievements;
        #endregion

        #region public methods
        public void RewardAchievement(Achievement achievement) {
            if (!IsRewardable(achievement)) {
                Debug.Log("Achievement \"" + achievement.Name + "\" already rewarded.");
                return;
            }

            if (SteamManager.Initialized)
                SteamUserStats.SetAchievement(achievement.SteamID);

            Debug.Log("Achievement \"" + achievement.Name + "\" rewarded.");
            _achievements.Remove(achievement);
        }

        public bool IsRewardable(Achievement achievement) => _achievements.Contains(achievement);
        #endregion

        #region private methods
        private void Awake() {
            if (_instance == null) {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        #endregion
    }

}

