using UnityEngine;

namespace Labyrinth.Achievements.Tracker {

    public class PayRespectsTracker : MonoBehaviour, IAchievementTracker {

        #region private fields
        [SerializeReference] private Achievement _achievement;

        private Camera _playerCamera;

        private float _maxDistance = 3.0f;
        #endregion

        #region public methods
        public void Track() {
            if (Input.GetKeyDown(KeyCode.F)) {
                var camera = transform.parent.GetComponentInChildren<Camera>();
                var ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance)) {
                    if (hit.transform.TryGetComponent(out Interactables.DecayedSoul decayedSoul)) {
                        decayedSoul.Interact();
                        AchievementManager.Instance.RewardAchievement(_achievement);
                        Destroy(this);
                    }
                }
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