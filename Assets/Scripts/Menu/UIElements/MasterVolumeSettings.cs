using UnityEngine;

namespace Labyrinth.Menu.UIElements {

	public class MasterVolumeSettings : VolumeSettings {

		protected override void UpdateSetting(float volume) {
			Core.AudioManager.Instance.MasterVolume = volume;
		}

		private void Awake() {
			_slider.value = Core.AudioManager.Instance.MasterVolume;
		}
	}

}