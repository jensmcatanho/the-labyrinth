using UnityEngine;

namespace Achievements {

    public class PayRespectsTracker : MonoBehaviour, IAchievementTracker {

        #region private fields
        [SerializeReference] private Achievement _achievement;
        #endregion

        #region public methods
        public void Track() {
            if (Input.GetKeyDown(KeyCode.F)) {
                AchievementManager.Instance.RewardAchievement(_achievement);
                Destroy(this);
            }
        }
        #endregion

        #region private methods
        private void Awake() {
            DestroyIfRewarded();
        }

        private void DestroyIfRewarded() {
            if (!AchievementManager.Instance.IsRewardable(_achievement))
                Destroy(this);
        }

        private void Update() {
            Track();
        }
        #endregion
    }

}