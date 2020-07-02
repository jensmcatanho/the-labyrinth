using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Core {

    public class DayCycle : MonoBehaviour {

        [SerializeField] private Light _sun;

        [SerializeField] private Light _moon;

        [SerializeField] [Range(0, 24)] private float _timeOfDay;

        [SerializeField] private float _minutesPerFrame;

        private bool _isDay;

        private void Awake() {
            _timeOfDay = Random.Range(0.0f, 24.0f);
            _isDay = _timeOfDay > 6.0f && _timeOfDay < 18.0f;

            if (_isDay)
                _moon.enabled = false;
            else
                _sun.enabled = false;

        }

        private void Update() {
            UpdateTime();
            
            RotateSun();
            RotateMoon();

            if (IsDawn()) {
                StartDay();

            } else if (IsDusk()) {
                StartNight();
            }
        }

        private void UpdateTime() {
            _timeOfDay += _minutesPerFrame / 60.0f;
            _timeOfDay = _timeOfDay > 24.0f ? 0.0f : _timeOfDay;
        }

        private void RotateSun() {
            _sun.transform.rotation = Quaternion.Euler(Mathf.Lerp(-90, 270, _timeOfDay / 24.0f), -150.0f, 0);
        }

        private void RotateMoon() {
            _moon.transform.rotation = Quaternion.Euler(Mathf.Lerp(-90, 270, _timeOfDay / 24.0f) - 180.0f, -150.0f, 0);
        }

        private bool IsDawn() {
            return !_isDay && _moon.transform.rotation.eulerAngles.x > 160.0f;
        }

        private bool IsDusk() {
            return _isDay && _sun.transform.rotation.eulerAngles.x > 160.0f;
        }

        private void StartDay() {
            _sun.enabled = true;
            _moon.enabled = false;
            _isDay = true;
        }

        private void StartNight() {
            _sun.enabled = false;
            _moon.enabled = true;
            _isDay = false;
        }
    }

}