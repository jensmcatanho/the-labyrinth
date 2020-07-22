using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Labyrinth.Menu.Settings.UIElements {

	public class VolumeSettings : MonoBehaviour {

		[SerializeField] protected Toggle _toggle;

		[SerializeField] protected Slider _slider;

		[SerializeField] protected TMP_Text _text;

		[SerializeField] protected float _volume = 100.0f;

		public void SetMute(bool isOn) {
			if (isOn) {
				UpdateSetting(_volume);
				_slider.value = _volume;

			} else {
				_volume = Core.AudioManager.Instance.MasterVolume;
				_slider.value = 0.0f;
			}
		}

		public void SetVolume(float volume) {
			SetText(volume);
			_toggle.isOn = volume > 0.0f;
			UpdateSetting(volume);
		}

		protected void SetText(float newVolume) {
			var volume = (int)newVolume;
			_text.text = volume.ToString();
		}

		protected virtual void UpdateSetting(float volume) {
			return;
		}
    }

}