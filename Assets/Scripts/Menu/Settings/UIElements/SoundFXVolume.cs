namespace Labyrinth.Menu.Settings.UIElements {

	public class SoundFXVolume : VolumeSettings {

		protected override void UpdateSetting(float volume) {
			Core.AudioManager.Instance.SoundFXVolume = volume;
		}

		private void Awake() {
			_slider.value = Core.AudioManager.Instance.SoundFXVolume;
		}

	}

}