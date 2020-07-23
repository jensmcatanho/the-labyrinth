namespace Labyrinth.Menu.Settings.UIElements {

	public class MusicVolume : VolumeSettings {

		protected override void UpdateSetting(float volume) {
			Core.AudioManager.Instance.MusicVolume = volume;
		}

		private void Awake() {
			_slider.value = Core.AudioManager.Instance.MusicVolume;
		}

	}

}